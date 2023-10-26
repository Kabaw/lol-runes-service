using Lol_Runes_Service.Domain.Entiies;
using Lol_Runes_Service.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lol_Runes_Service.Infra.Repositories;

public class RuneStoneRepository : BaseRepository<RuneStone>, IRuneStoneRepository, IRepository<RuneStone>
{
    public RuneStoneRepository(DataContext dataContext, DbSet<RuneStone> db) : base(dataContext, db)
    {
    }

    public string GetInfo()
    {
        return "It is working";
    }
}