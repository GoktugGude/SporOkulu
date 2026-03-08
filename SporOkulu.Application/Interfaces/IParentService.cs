using System;
using SporOkulu.Application.DTOs.ParentDTOs;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Application.Interfaces;

public interface IParentService : IGenericService<Parent,DetailParentDto,CreateParentDto,UpdateParentDto>
{

}
