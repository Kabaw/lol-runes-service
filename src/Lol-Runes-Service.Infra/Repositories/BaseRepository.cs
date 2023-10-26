using Lol_Runes_Service.Domain.Filters;
using Lol_Runes_Service.Domain.Models;
using Lol_Runes_Service.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Lol_Runes_Service.Infra.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
    where T : Entity
{
    private IEnumerable<PropertyInfo>? matchProperties = null;

    protected DbSet<T> db;
    protected DataContext dataContext;

    public BaseRepository(DataContext dataContext, DbSet<T> db)
    {
        this.db = db;
        this.dataContext = dataContext;
    }

    public long Add(T entity)
    {
        db.Add(entity);
        dataContext.SaveChanges();

        return entity.Id;
    }

    public void Update(T entity)
    {
        db.Update(entity);
        dataContext.SaveChanges();
    }

    public void Delete(long id)
    {
        //db.Where(x => x.Id == id).ExecuteDelete();
        //dataContext.SaveChanges();
    }

    public bool Exists(long id)
    {
        return db.Any(x => x.Id == id);
    }

    public T? Get(long id)
    {
        return db.FirstOrDefault(c => c.Id == id);
    }

    public T? GetAsNoTrack(long id)
    {
        return db.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public IQueryable<T> GetAll()
    {
        return db;
    }

    public virtual IQueryable<T> GetFiltered(BaseFilter<T> filter)
    {
        IdentifyMatchProperties(filter);

        var query = db.AsNoTracking();

        foreach (var prop in matchProperties!)
        {
            if (prop.GetValue(filter) is not null)
                query = query.Where(GetFilterQuerySection(filter, prop));
        }

        return query;
    }

    private void IdentifyMatchProperties(BaseFilter<T> filter)
    {
        if (matchProperties is not null)
            return;

        var entityType = typeof(T);
        var entityProperties = entityType.GetProperties();
        var filterProperties = filter.GetType().GetProperties();
        matchProperties = filterProperties.Where(p => entityProperties.Any(p2 => p2.Name == p.Name));
    }

    private Expression<Func<T, bool>> GetFilterQuerySection(BaseFilter<T> filter, PropertyInfo matchProperty)
    {
        var item = Expression.Parameter(typeof(T), "item");
        var prop = Expression.Property(item, matchProperty.Name);
        var value = Expression.Constant(matchProperty.GetValue(filter));
        var equal = Expression.Equal(prop, value);

        return Expression.Lambda<Func<T, bool>>(equal, item);
    }
}