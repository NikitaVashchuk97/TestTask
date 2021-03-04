using Microsoft.EntityFrameworkCore;

namespace Test_task.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            if (!Database.CanConnect())
            {
                Database.Migrate();
                Add(new User { Name = "Иван", Surname = "Иванов" });
                Add(new User { Name = "Петр", Surname = "Петров" });
                SaveChanges();
                return;
            }
            Database.Migrate();

        }
    }
}
