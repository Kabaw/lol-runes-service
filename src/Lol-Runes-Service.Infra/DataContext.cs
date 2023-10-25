using Microsoft.EntityFrameworkCore;

namespace Lol_Runes_Service.Infra;

public class DataContext : DbContext
{
    //public DbSet<Customer> Customers { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
    {
    }
}