using ConsoleApp5.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp5.Dbcontext;

public class AppDbContext:DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
    }
}
