using System;

namespace SporOkulu.Domain.Interfaces;

public interface IStudentRepository : IGenericRepository<Student>
{
    Task<List<Student>> GetStudentsAsync();
    Task<Student?> GetStudentAsync(int id);
}
