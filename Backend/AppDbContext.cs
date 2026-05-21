using ConcurrentJobProcessor.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcurrentJobProcessor
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Products> Products => Set<Products>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Categories).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("numeric(18,2)");
            });
        }
    }
}
