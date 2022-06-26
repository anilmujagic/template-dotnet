using System.Linq.Expressions;

namespace MyApp.Core.Interfaces;

public interface IRepository<T>
    where T : class
{
    void Insert(T entity);
    void Insert(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void Delete(IEnumerable<T> entities);

    Task<T?> GetByKey(params object[] keyValues);

    Task<bool> Exists(Expression<Func<T, bool>> whereCondition);

    #region GetCount

    Task<int> GetCount();
    Task<long> GetLongCount();
    Task<int> GetCount(Expression<Func<T, bool>> whereCondition);
    Task<long> GetLongCount(Expression<Func<T, bool>> whereCondition);

    #endregion

    #region GetAll

    Task<IEnumerable<T>> GetAll();

    Task<IEnumerable<T>> GetAll<TInclude1>(
        Expression<Func<T, TInclude1>> includeProperty1);

    Task<IEnumerable<T>> GetAll<TInclude1, TInclude2>(
        Expression<Func<T, TInclude1>> includeProperty1,
        Expression<Func<T, TInclude2>> includeProperty2);

    Task<IEnumerable<T>> GetAll<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, TInclude1>> includeProperty1,
        Expression<Func<T, TInclude2>> includeProperty2,
        Expression<Func<T, TInclude3>> includeProperty3);

    #endregion

    #region Get

    Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>> whereCondition);

    Task<IEnumerable<T>> Get<TInclude1>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, TInclude1>> includeProperty1);

    Task<IEnumerable<T>> Get<TInclude1, TInclude2>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, TInclude1>> includeProperty1,
        Expression<Func<T, TInclude2>> includeProperty2);

    Task<IEnumerable<T>> Get<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, TInclude1>> includeProperty1,
        Expression<Func<T, TInclude2>> includeProperty2,
        Expression<Func<T, TInclude3>> includeProperty3);

    #endregion

    #region GetDeep

    Task<IEnumerable<T>> GetDeep<TInclude1, TInclude2>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, ICollection<TInclude1>>> includeProperty1,
        Expression<Func<TInclude1, TInclude2>> includeProperty2);

    Task<IEnumerable<T>> GetDeep<TInclude1, TInclude2, TInclude3>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, ICollection<TInclude1>>> includeProperty1,
        Expression<Func<TInclude1, ICollection<TInclude2>>> includeProperty2,
        Expression<Func<TInclude2, TInclude3>> includeProperty3);

    #endregion

    Task<IEnumerable<TOutput>> GetAs<TOutput>(
        Expression<Func<T, bool>> whereCondition,
        Expression<Func<T, TOutput>> mapFunction);
}
