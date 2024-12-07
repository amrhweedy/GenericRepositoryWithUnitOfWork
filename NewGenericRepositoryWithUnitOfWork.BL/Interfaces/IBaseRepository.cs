using NewGenericRepositoryWithUnitOfWork.BL.Const;
using System.Linq.Expressions;

namespace NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);

    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
      string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderByDirection.Ascending);

    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
     string[] includes = null, string orderByField = null, string orderByDirection = OrderByDirection.Ascending);

    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);


}
