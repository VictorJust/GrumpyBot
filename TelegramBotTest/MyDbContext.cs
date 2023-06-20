using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace TelegramBotTest
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // Other properties specific to a user
    }

    public class MyDbContext : DbContext
    {
        public MyDbContext()
    : base(new DbContextOptionsBuilder<MyDbContext>().UseMySQL("server=127.0.0.1;database=mydatabase;user=root;password=db14_06MyTry").Options)
        {
        }


        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }

        // Method to retrieve a User entity by ID
        public async Task<User> GetUserById(int id)
        {
            return await Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;database=mydatabase;user=root;password=db14_06MyTry");
        }
    }
}
