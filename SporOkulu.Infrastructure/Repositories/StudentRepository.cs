using System;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;

namespace SporOkulu.Infrastructure.Repositories;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Student?> GetStudentAsync(int id)
    {
         return await _context.Students
            .Include(s => s.AppUser)
            .Include(s => s.Parent)
                .ThenInclude(p => p.AppUser)
            .Include(s => s.Branch)
            .Include(s => s.Payments)
            .Include(s => s.TestResults)
                .ThenInclude(t => t.TestType).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _context.Students
            .Include(s => s.AppUser)
            .Include(s => s.Parent)
                .ThenInclude(p => p.AppUser)
            .Include(s => s.Branch)
            .Include(s => s.Payments)
            .Include(s => s.TestResults)
                .ThenInclude(t => t.TestType)
            
                
            .ToListAsync();
    }
}
