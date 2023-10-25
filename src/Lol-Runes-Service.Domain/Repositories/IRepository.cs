using Lol_Runes_Service.Domain.Filters;
using Lol_Runes_Service.Domain.Models;

namespace Lol_Runes_Service.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    long Add(T entity);
    void Update(T entity);
    void Delete(long id);
    bool Exists(long id);
    T? Get(long id);
    T? GetAsNoTrack(long id);
    IQueryable<T> GetAll();
    virtual IQueryable<T> GetFiltered(BaseFilter<T> filter) { return Enumerable.Empty<T>().AsQueryable(); }
}