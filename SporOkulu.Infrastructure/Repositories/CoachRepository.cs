using System;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;

namespace SporOkulu.Infrastructure.Repositories;

public class CoachRepository : GenericRepository<Coach>, ICoachRepository
{
    public CoachRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Coach?> GetCoachAsync(int id)
    {
       return await _context.Coaches
            .Include(x => x.AppUser)
            .Include(x => x.Branch).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Coach>> GetCoachesAsync()
    {
        return await _context.Coaches
            .Include(x => x.AppUser)
            .Include(x => x.Branch)
            .ToListAsync();

    }
}
