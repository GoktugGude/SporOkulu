using System;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Infrastructure.Repositories;

public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
{
    public AttendanceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Student>> GetStudentsByBranchAsync(int branchId)
    {
        return await _context.Students
            .Include(x => x.AppUser)
            .Where(x => x.BranchId == branchId)
            .ToListAsync();
    }

    public async Task AddRangeAsync(IEnumerable<Attendance> attendances)
{
    await _context.Attendances.AddRangeAsync(attendances);
    await _context.SaveChangesAsync();
}
}
