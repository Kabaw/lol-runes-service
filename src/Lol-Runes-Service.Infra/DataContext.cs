using Lol_Runes_Service.Domain.Entiies;
using Microsoft.EntityFrameworkCore;

namespace Lol_Runes_Service.Infra;

public class DataContext : DbContext
{
    public DbSet<RuneStone> RuneStones { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
    {
    }
}