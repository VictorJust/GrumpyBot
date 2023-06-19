using Microsoft.EntityFrameworkCore;

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
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }

        // Method to retrieve a User entity by ID
        public async Task<User> GetUserById(int id)
        {
            return await Users.FindAsync(id);
        }
    }
}
