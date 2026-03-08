using System;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Domain.Interfaces;

public interface IParentRepository
{
    Task<List<Parent>> GetParentsAsync();
    Task<Parent?> GetParentAsync(int id);
}
