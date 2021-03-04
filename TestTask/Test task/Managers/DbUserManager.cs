using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public DbUserManager(UsersContext context)
        {
            this.db = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
