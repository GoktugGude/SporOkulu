using System;
using System.Linq.Expressions;

namespace SporOkulu.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync2(params string[] includes);
    Task<T> GetByIdAsync(int id);
   IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
    Task<int> SaveChangesAsync();
    Task AddRangeAsync(IEnumerable<T> entities);
}
