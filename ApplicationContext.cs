using Microsoft.EntityFrameworkCore;
using NetCorePractice.Models;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users => Set<UserModel>();
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=PostgreSQL-16;Port=5432;Database=userDb;Username=postgres;Password=postgres");
    }
}
