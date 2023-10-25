using Lol_Runes_Service.Domain.Models;

namespace Lol_Runes_Service.Domain.Filters;

public class BaseFilter<T> where T : Entity
{
    public long Id { get; set; }
}