using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data.EF;

namespace MyApp.Infrastructure.Data;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly AppDb _db;

    public Repository(AppDb db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public void Insert(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    public void Insert(IEnumerable<T> entities)
    {
        _db.ChangeTracker.AutoDetectChangesEnabled = false;
        _db.Set<T>().AddRange(entities);
        _db.ChangeTracker.AutoDetectChangesEnabled = true;
    }

    public void Update(T entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        if (_db.Entry(entity).State == EntityState.Detached)
            _db.Entry(entity).State = EntityState.Unchanged;
        _db.Set<T>().Remove(entity);
    }

    public void Delete(IEnumerable<T> entities)
    {
        foreach(var entity in entities)
        {
            Delete(entity);
        }
    }

    public async Task<T?> GetByKey(params object[] keyValues)
    {
        return await _db.Set<T>().FindAsync(keyValues);
    }

    public Task<bool> Exists(Expression<Func<T, bool>> whereCondition)
    {
        return _db.Set<T>().AnyAsync(whereCondition);
    }

    #region GetCount

    public Task<int> GetCount()
    {
        return _db.Set<T>().CountAsync();
    }

    public Task<long> GetLongCount()
    {
        return _db.Set<T>().LongCountAsync();
    }

    public Task<int> GetCount(Expression<Func<T, bool>> whereCondition)
    {
        return _db.Set<T>().CountAsync(whereCondition);
    }

    public Task<long> GetLongCount(Expression<Func<T, bool>> whereCondition)
    {
        return _db.Set<T>().LongCountAsync(whereCondition);
    }

    #endregion

    #region GetAll

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _db.Set<T>().ToListAsync();
    }

    public Task<IEnumerable<T>> GetAll<TInclude1>(
        Expression<Func<T, TInclude1>> includeProperty1)
    {
        return this.Get(null, includeProperty1);
    }

    public Task<IEnumerable<T>> GetAll<TInclude1, TInclude2>(
        Expression<Func<T, TInclude1>> includeProperty1,
        Expression<Func<T, TInclude2>> includeProperty2)
    {
        return this.Get(null, includeProperty1, includeProperty2);
    }

    public Task<IEnumerable<T>> GetAll<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, TInclude1>> includeProperty1,
        Expression<Func<T, TInclude2>> includeProperty2,
        Expression<Func<T, TInclude3>> includeProperty3)
    {
        return this.Get(null, includeProperty1, includeProperty2, includeProperty3);
    }

    #endregion

    #region Get

    public async Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>> whereCondition)
    {
        return await this.Get<object>(whereCondition, null);
    }

    public async Task<IEnumerable<T>> Get<TInclude1>(
        Expression<Func<T, bool>>? whereCondition,
        Expression<Func<T, TInclude1>>? includeProperty1)
    {
        return await this.Get<TInclude1, object>(whereCondition, includeProperty1, null);
    }

    public async Task<IEnumerable<T>> Get<TInclude1, TInclude2>(
        Expression<Func<T, bool>>? whereCondition,
        Expression<Func<T, TInclude1>>? includeProperty1,
        Expression<Func<T, TInclude2>>? includeProperty2)
    {
        return await this.Get<TInclude1, TInclude2, object>(whereCondition, includeProperty1, includeProperty2, null);
    }

    public async Task<IEnumerable<T>> Get<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, bool>>? whereCondition,
        Expression<Func<T, TInclude1>>? includeProperty1,
        Expression<Func<T, TInclude2>>? includeProperty2,
        Expression<Func<T, TInclude3>>? includeProperty3)
    {
        return await this.GetQuery(whereCondition, includeProperty1, includeProperty2, includeProperty3)
            .ToListAsync();
    }

    // Breadth traversal of navigation properties
    private IQueryable<T> GetQuery<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, bool>>? whereCondition,
        Expression<Func<T, TInclude1>>? includeProperty1,
        Expression<Func<T, TInclude2>>? includeProperty2,
        Expression<Func<T, TInclude3>>? includeProperty3)
    {
        var query = _db.Set<T>().AsQueryable();

        if (includeProperty1 != null)
            query = query.Include(includeProperty1);
        if (includeProperty2 != null)
            query = query.Include(includeProperty2);
        if (includeProperty3 != null)
            query = query.Include(includeProperty3);

        if (whereCondition != null)
            query = query.Where(whereCondition);

        return query;
    }

    #endregion

    #region GetDeep

    public async Task<IEnumerable<T>> GetDeep<TInclude1, TInclude2>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, ICollection<TInclude1>>> includeProperty1,
        Expression<Func<TInclude1, TInclude2>> includeProperty2)
    {
        return await this.GetDeepQuery(whereCondition, includeProperty1, includeProperty2)
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetDeep<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, ICollection<TInclude1>>> includeProperty1,
        Expression<Func<TInclude1, ICollection<TInclude2>>> includeProperty2,
        Expression<Func<TInclude2, TInclude3>> includeProperty3)
    {
        return await this.GetDeepQuery(whereCondition, includeProperty1, includeProperty2, includeProperty3)
            .ToListAsync();
    }

    // Depth traversal of navigation properties (2 levels)
    protected internal IQueryable<T> GetDeepQuery<TInclude1, TInclude2>(
        Expression<Func<T, bool>>? whereCondition,
        Expression<Func<T, ICollection<TInclude1>>>? includeProperty1,
        Expression<Func<TInclude1, TInclude2>>? includeProperty2)
    {
        var query = _db.Set<T>().AsQueryable();

        if (includeProperty1 != null)
            query = query.Include(includeProperty1);
        if (includeProperty2 != null)
            query = ((IIncludableQueryable<T, ICollection<TInclude1>>)query).ThenInclude(includeProperty2);

        if (whereCondition != null)
            query = query.Where(whereCondition);

        return query;
    }

    // Depth traversal of navigation properties (3 levels)
    protected internal IQueryable<T> GetDeepQuery<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, bool>>? whereCondition,
        Expression<Func<T, ICollection<TInclude1>>>? includeProperty1,
        Expression<Func<TInclude1, ICollection<TInclude2>>>? includeProperty2,
        Expression<Func<TInclude2, TInclude3>>? includeProperty3)
    {
        var query = _db.Set<T>().AsQueryable();

        if (includeProperty1 != null)
            query = query.Include(includeProperty1);
        if (includeProperty2 != null)
            query = ((IIncludableQueryable<T, ICollection<TInclude1>>)query).ThenInclude(includeProperty2);
        if (includeProperty3 != null)
            query = ((IIncludableQueryable<T, ICollection<TInclude2>>)query).ThenInclude(includeProperty3);

        if (whereCondition != null)
            query = query.Where(whereCondition);

        return query;
    }

    #endregion

    public async Task<IEnumerable<TOutput>> GetAs<TOutput>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, TOutput>> mapFunction)
    {
        return await _db.Set<T>()
            .Where(whereCondition)
            .Select(mapFunction)
            .ToListAsync();
    }
}
