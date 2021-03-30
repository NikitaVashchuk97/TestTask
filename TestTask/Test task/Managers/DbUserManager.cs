using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_task.Models;

namespace Test_task.Controllers
{
    public class DbUserManager : IUserManager
    {
        private UsersContext db;

        private readonly ILogger<DbUserManager> _logger;
        public DbUserManager(UsersContext context, ILogger<DbUserManager> logger = null)
        {
            db = context;

            _logger = logger;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users =  await db.Users.ToListAsync();

            _logger.LogInformation("Вызван метод db.Users.ToListAsync()");

            return users;
        }

        public async Task<User> CreateUser(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            _logger.LogInformation("Вызван метод db.Users.AddAsync(user) где user : {user}");

            return user;
        }

        public async Task Update(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();

            _logger.LogInformation("Вызван метод db.Users.Update(user); где user : {user}");
        }

        public async ValueTask<User> GetUserByMail(string email)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Mail == email);
        }

        public async ValueTask<User> GetUserById(int id)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
