using Microsoft.EntityFrameworkCore;
using RedisPractice.Domain.Entity;

namespace Infrastructure.Postgres.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasKey(m => m.Id);
    }
}