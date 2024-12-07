
using Microsoft.EntityFrameworkCore;
using NewGenericRepositoryWithUnitOfWork.BL.Const;
using NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
using NewGenericRepositoryWithUnitOfWork.DAL;
using System.Linq.Expressions;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
{

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
    {
        IQueryable<T> query = context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(criteria);

    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
    {
        IQueryable<T> query = context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Where(criteria).ToListAsync();

    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take)
    {
        return await context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take = 3, string[] includes = null,
        Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderByDirection.Ascending)
    {
        IQueryable<T> query = context.Set<T>().Where(criteria).Skip(skip ?? 0).Take(take ?? 3);

        if (orderBy != null)
        {
            if (orderByDirection == OrderByDirection.Ascending)
            {
                query = query.OrderBy(orderBy);

            }
            else if (orderByDirection == OrderByDirection.Descending)
            {
                query = query.OrderByDescending(orderBy);
            }
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }


        return await query.ToListAsync();

    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take, string[] includes = null, string orderByField = null, string orderByDirection = "ASC")
    {
        IQueryable<T> query = context.Set<T>().Where(criteria).Skip(skip ?? 0).Take(take ?? 3);

        Expression<Func<T, object>> orderBy = null;

        if (!string.IsNullOrEmpty(orderByField))
        {
            switch (orderByField.ToLower())
            {
                case "id":
                    orderBy = (x => EF.Property<object>(x, "Id"));
                    break;

                case "title":
                    orderBy = (x => EF.Property<object>(x, "Title"));
                    break;

                default:
                    orderBy = (x => EF.Property<object>(x, "Id"));
                    break;
            }
        }

        if (!string.IsNullOrEmpty(orderByDirection))
        {
            if (orderByDirection == OrderByDirection.Ascending)
            {
                query = query.OrderBy(orderBy);
            }
            else if (orderByDirection == OrderByDirection.Descending)
            {
                query = query.OrderByDescending(orderBy);
            }
        }

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }


        return await query.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        //  await context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {

        await context.Set<T>().AddRangeAsync(entities);
        // await context.SaveChangesAsync();

        return entities;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}


