using System;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Domain.Interfaces;

public interface IParentRepository : IGenericRepository<Parent>
{
    Task<List<Parent>> GetParentsAsync();
    Task<Parent?> GetParentAsync(int id);
}
