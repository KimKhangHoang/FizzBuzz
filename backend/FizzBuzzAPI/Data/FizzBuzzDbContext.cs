using Microsoft.EntityFrameworkCore;
using FizzBuzzAPI.Models;

namespace FizzBuzzAPI.Data
{
    public class FizzBuzzDbContext : DbContext
    {
        public FizzBuzzDbContext(DbContextOptions<FizzBuzzDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameRule> GameRules { get; set; }
        public DbSet<Rule> Rules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship
            modelBuilder.Entity<GameRule>()
                .HasMany(g => g.Rules)
                .WithOne()
                .HasForeignKey(r => r.GameRuleId);
        }
    }
}
