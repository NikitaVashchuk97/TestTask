using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test_task.Loggers;

namespace Test_task.Models
{
    public class UsersContext : DbContext
    {       
        public DbSet<User> Users { get; set; }

        private static ILoggerFactory myLoggerFactory;     

        public UsersContext(DbContextOptions<UsersContext> options, ILogger<UsersContext> logger = null)
            : base(options)
        {
            if(myLoggerFactory == null)
                myLoggerFactory = new LoggerFactory(new[] {new MyLoggerProvider(logger) });

            Database.Migrate();          
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Иван", Surname = "Иванов", Language = "en-Us", Mail = "www.vbsda@a.ru", Password = "265676", IsAdmin = true },
                new User { Id = 2, Name = "Петр", Surname = "Петров", Language = "en-Us", Mail = "www.vbsda@a.ru", Password = "265676", IsAdmin = true });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(myLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
