using Microsoft.EntityFrameworkCore;
namespace SearchUserAPI.Models
{
    /// <summary>
    /// Database Context class
    /// </summary>
    public class UserDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">DbContextOptions<UserDbContext></param>
        public UserDbContext(DbContextOptions<UserDbContext> options)
       : base(options)
        {
        }

        /// <summary>
        /// User Entity
        /// </summary>
        public DbSet<User> User { get; set; }
    }
}