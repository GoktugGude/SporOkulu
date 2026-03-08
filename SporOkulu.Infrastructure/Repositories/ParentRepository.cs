using System;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;

namespace SporOkulu.Infrastructure.Repositories;

public class ParentRepository : GenericRepository<Parent>, IParentRepository
{
    public ParentRepository(AppDbContext context) : base(context)
    {
    }
    

    public async Task<List<Parent>> GetParentsAsync()
    {
        var data = await _context.Set<Parent>().Include(x => x.AppUser).ToListAsync();
foreach(var p in data)
{
    Console.WriteLine($"Parent: {p.AppUser?.FirstName ?? "NULL USER"}");
}
        return await _context.Set<Parent>()
            .Include(x => x.AppUser)
            .Include(x => x.Students)
                .ThenInclude(x => x.AppUser)
            .ToListAsync();
    }

    public async  Task<Parent?> GetParentAsync(int id)
    {
        return await _context.Set<Parent>()
            .Include(x => x.AppUser)
            .Include(x => x.Students)
            .ThenInclude(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            
    }
}
