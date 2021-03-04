using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_task.Models
{
    public interface IRepository
    {
        Task<IEnumerable<User>> GetAll();
        User GetUser(int id) { throw new NotImplementedException(); }       
        void Create(User user);
        void Save() { throw new NotImplementedException(); }          
        void Init();
    }
}
