using System;
using SporOkulu.Application.DTOs.CoachDTOs;
using SporOkulu.Application.Services;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Application.Interfaces;

public interface ICoachService : IGenericService<Coach,DetailCoachDto,CreateCoachDto,UpdateCoachDto>
{
    
}
