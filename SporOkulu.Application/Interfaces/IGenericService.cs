using System;
using System.Linq.Expressions;
using SporOkulu.Application.DTOs.ResponseDTOs;

namespace SporOkulu.Application.Interfaces;

public interface IGenericService<TEntity, TDto,TCreateDto,TUpdateDto > 
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<ResponseDto<List<TDto>>> GetAllAsync();
    Task<ResponseDto<TDto>> GetByIdAsync(int id);
    Task<ResponseDto<object>> AddAsync(TCreateDto dto);
    Task<ResponseDto<object>> UpdateAsync(TUpdateDto dto);
    Task<ResponseDto<object>> DeleteAsync(int id);
    Task<ResponseDto<List<TDto>>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
}
