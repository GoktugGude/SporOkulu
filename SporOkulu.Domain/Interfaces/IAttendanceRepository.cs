using System;

namespace SporOkulu.Domain.Interfaces;

public interface IAttendanceRepository : IGenericRepository<Attendance>
{
    Task<List<Student>> GetStudentsByBranchAsync(int branchId);
    Task AddRangeAsync(IEnumerable<Attendance> attendances);

}
