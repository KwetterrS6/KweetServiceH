using KweetService.Models;
using Microsoft.EntityFrameworkCore;

namespace KweetService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Kweet> Kweets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Kweets)
                .WithOne(p=> p.User!)
                .HasForeignKey(p => p.UserId);

            modelBuilder
                .Entity<Kweet>()
                .HasOne(p => p.User)
                .WithMany(p => p.Kweets)
                .HasForeignKey(p =>p.UserId);
        }
    }
}