using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Test_task.Models;


namespace Test_task.Controllers
{
    public class FileUserManager : IUserManager
    {      
        private string path;
        public FileUserManager(IConfiguration configuration)
        {
            this.path = configuration.GetConnectionString("DefaultConnectionJson");
        }

        public async Task<List<User>> GetAllUsers() 
        {
            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            var users = await JsonSerializer.DeserializeAsync<List<User>>(fs);

            return users;
        }

        public async Task<User> CreateUser(User user)
        {
            var users = await GetAllUsers();
            users.Add(user);

            using FileStream fs = new FileStream(path, FileMode.Create);
            await JsonSerializer.SerializeAsync<List<User>>(fs, users);

            return user;
        }

        public async void InitJson()
        {
            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            if (fs.Length == 0)
            {
                List<User> users = new List<User>();
                users.Add(new User { Id = 0, Name = "nik", Surname = "weqw21" });
                users.Add(new User { Id = 1, Name = "nik1", Surname = "weqw223131231" });
                users.Add(new User { Id = 2, Name = "nik2", Surname = "weqw22131231" });

                await JsonSerializer.SerializeAsync<List<User>>(fs, users);
            }
        }

        public async Task Update(User user)
        {
            var users = await GetAllUsers();
            var foundUserIndex = users.IndexOf(users.FirstOrDefault(u => u.Id == user.Id));
            users[foundUserIndex] = user;

            using FileStream fs = new FileStream(path, FileMode.Create);
            await JsonSerializer.SerializeAsync<List<User>>(fs, users);
        }

        public async ValueTask<User> GetUserByMail(string email)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(user => user.Mail == email);
        }

        public async ValueTask<User> GetUserById(int id)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(user => user.Id == id);
        }
    }
}
