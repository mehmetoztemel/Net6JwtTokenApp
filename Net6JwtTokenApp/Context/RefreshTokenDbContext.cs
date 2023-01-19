using Microsoft.EntityFrameworkCore;
using Net6JwtTokenApp.Models;

namespace Net6JwtTokenApp.Context
{
    public class RefreshTokenDbContext : DbContext
    {
        public RefreshTokenDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin"
            });
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Username = "demo",
                    Password = "demo"
                });
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
