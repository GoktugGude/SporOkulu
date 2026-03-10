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
        return await _context.Set<Student>()
            .Include(x => x.AppUser)
            .Where(x => x.BranchId == branchId)
            .ToListAsync();
    }
//     public async Task<List<Student>> GetStudentsByBranchAsync(int branchId)
// {
//     // Sorguyu parçalara ayırarak hatanın nerede olduğunu bulalım
//     var query = _context.Set<Students>
//         .Include(x => x.AppUser) // AppUser'ı dahil et
//         .Where(x => x.BranchId == branchId);

//     // SQL sorgusunu konsola yazdır (Debug etmek için)
//     Console.WriteLine("Sorgu oluşturuldu.");

//     // Listeye çevirirken hata oluşursa burası patlar
//     var list = await query.ToListAsync();
    
//     Console.WriteLine($"Bulunan öğrenci sayısı: {list.Count}");
    
//     return list;
// }

    public async Task AddRangeAsync(IEnumerable<Attendance> attendances)
{
    await _context.Set<Attendance>().AddRangeAsync(attendances);
    // await _context.SaveChangesAsync();
}
}
