using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Test_task.Controllers;
using Test_task.Models;

namespace Test_task.Tests
{
    public class UnitTestsUser
    {
        private IConfiguration conf;
        private User testUser;
        private List<User> testSetUsers;
        private IUserManager UserManager;

        [SetUp]
        public async Task SetUpJson()
        {
            var fullPath = Path.GetFullPath(@"..\..\..\testConfig.json");
            var builder = new ConfigurationBuilder().AddJsonFile(fullPath);
            conf = builder.Build();

            string connection = conf.GetConnectionString("DefaultConnectionDb");

            UserManager = new FileUserManager(conf);

            //var contextOptions = new DbContextOptionsBuilder<UsersContext>()
            //    .UseMySql("Server=localhost;Port=3306;User=root;Password=265676-333AAAaaa;Database=testDb;",
            //    new MySqlServerVersion(new Version(8, 0, 11))
            //    ).Options;

            //var context = new UsersContext(contextOptions);

            //InitDb(context);

            //UserManager = new DbUserManager(context);

            testUser = new User { Id = 4, Name = "Nikita", Surname = "Surname", Mail = "www.asdh-98@mail.ru", Password = "232Aa" };

            testSetUsers = new List<User> {
                new User { Id = 1, Name = "nik", Surname = "nik3", Mail = "www.asdh-98@mail.ru", Password = "232Aa" },
                new User { Id = 2, Name = "nik1", Surname = "nik4", Mail = "www.asdh-101@mail.ru", Password = "2321Aa" },
                new User { Id = 3, Name = "nik2", Surname = "nik5", Mail = "www.asdh-12311@mail.ru", Password = "21Aa" }};

            await ClearAndInitJson("test.json", testSetUsers);

        }

        [Order(1)]
        [Test]
        public async Task TestGetAllUsers()
        {
            var users = await UserManager.GetAllUsers();

            CollectionAssert.AreEqual(testSetUsers, users);

        }

        [Order(2)]
        [Test]
        public async Task TestCreateUser()
        {
            await UserManager.CreateUser(testUser);

            var users = await UserManager.GetAllUsers();

            Assert.True(users.Exists(user => user == testUser));
        }

        private async Task ClearAndInitJson(string path, List<User> setUsers)
        {
            using FileStream fs = new FileStream(path, FileMode.Create);
            var users = new List<User>();
            users.AddRange(setUsers);
            await JsonSerializer.SerializeAsync<List<User>>(fs, users);
        }

        private void InitDb(UsersContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            context.AddRange(new List<User> {
            new User { Id = 1, Name = "nik", Surname = "nik3", Mail = "www.asdh-98@mail.ru", Password = "232Aa" },
            new User { Id = 2, Name = "nik1", Surname = "nik4", Mail = "www.asdh-101@mail.ru", Password = "2321Aa" },
            new User { Id = 3, Name = "nik2", Surname = "nik5", Mail = "www.asdh-12311@mail.ru", Password = "21Aa" }});

            context.SaveChanges();
        }
    }
}