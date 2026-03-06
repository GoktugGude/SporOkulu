using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;

namespace SporOkulu.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetAllAsync2(params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();
        if(includes != null)
        {
            foreach(var table in includes)
            {
                query = query.Include(table);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        return result;
       
    }

  public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
{
    return _context.Set<T>().Where(method);
}
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

 public async Task AddRangeAsync(IEnumerable<T> entities)
{
    await _context.Set<T>().AddRangeAsync(entities);
    await _context.SaveChangesAsync();
}
}
