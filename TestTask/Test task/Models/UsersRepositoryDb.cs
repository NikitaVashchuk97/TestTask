using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_task.Models
{
    public class UsersRepositoryDb : IRepository
    {
        private UsersContext db;
        public UsersRepositoryDb(UsersContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }
        public User GetUser(int id)
        {
            return db.Users.Find(id);
        }

        public async void Create(User c)
        {
           await db.Users.AddAsync(c);
        }

        public void Save()
        {
            db.SaveChanges();
        }
        
        public void Init()
        {
            return;
        }

    }
}
