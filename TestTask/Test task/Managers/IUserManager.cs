using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_task.Models;

namespace Test_task.Controllers
{
    public interface IUserManager 
    {
        public Task<List<User>> GetAllUsers();

        public Task<User> CreateUser(User user);


    }
}
