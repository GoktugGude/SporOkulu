using System;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Domain.Interfaces;

public interface ICoachRepository : IGenericRepository<Coach>
{
    Task<List<Coach>> GetCoachesAsync();
    Task<Coach?> GetCoachAsync(int id);
}
