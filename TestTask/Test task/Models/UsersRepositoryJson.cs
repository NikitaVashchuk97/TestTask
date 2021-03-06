﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test_task.Models
{
    public class UsersRepositoryJson : IRepository
    {

        private string path;
        public UsersRepositoryJson(IConfiguration configuration)
        {
            this.path = configuration.GetConnectionString("DefaultConnectionJson");
        }
        public async void Create(User user)
        {
            var users = (List<User>)GetAll().Result;
            users.Add(user);

            using FileStream fs = new FileStream(path, FileMode.Create);
            await JsonSerializer.SerializeAsync<List<User>>(fs, users);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            var users = await JsonSerializer.DeserializeAsync<List<User>>(fs);
            return users;
        }   

        public async void Init()
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
    }
}
