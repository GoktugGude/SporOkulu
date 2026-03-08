using System;
using System.Linq.Expressions;
using AutoMapper;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SporOkulu.Application.Services;

public class GenericManager<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
where TEntity : class
where TDto :class
where TCreateDto : class
where TUpdateDto :class

{

    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    private IParentRepository parentRepository;

    protected virtual string[] Includes => Array.Empty<string>();

    public GenericManager(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public GenericManager(IParentRepository parentRepository, IMapper mapper)
    {
        this.parentRepository = parentRepository;
        _mapper = mapper;
    }

    // GenericManager.cs içindeki GetAllAsync metodunu BU HALE GETİR:
    public virtual async Task<ResponseDto<List<TDto>>> GetAllAsync()
{
    // Eski GetAllAsync() yerine, Includes parametresi alan GetAllAsync2'yi çağırıyoruz.
    var entities = await _repository.GetAllAsync2(Includes);

    if (entities is null || !entities.Any()) 
        return ResponseDto<List<TDto>>.SuccessResult("Kayıt bulunamadı");

    var dtos = _mapper.Map<List<TDto>>(entities);
    return ResponseDto<List<TDto>>.SuccessResult(dtos);
}


    public async Task<ResponseDto<object>> AddAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
        var result = await _repository.SaveChangesAsync();
        if(result > 0)
            return ResponseDto<object>.SuccessResult("Kayıt başarıyla eklendi.");
        return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Kayıt sırasında bir hata oluştu");
    }

    public async Task<ResponseDto<object>> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if(entity == null)
            return ResponseDto<object>.ErrorResult(ErrorCodes.NotFound, "Silinecek bir kayıt bulunamadı.");

         _repository.Delete(entity);
        var result = await _repository.SaveChangesAsync();

        if(result > 0)
            return ResponseDto<object>.SuccessResult("Kayıt başarıyla silindi");
        
        return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Silme işlemi başarısız. ");
        }
    

    // public async Task<ResponseDto<List<TDto>>> GetAllAsync()
    // {
    //     var entites = await _repository.GetAllAsync();
    //     if(entites is null || !entites.Any()) return ResponseDto<List<TDto>>.SuccessResult("Kayıt bulunamadı");
    //     var dtos = _mapper.Map<List<TDto>>(entites);
    //     return ResponseDto<List<TDto>>.SuccessResult(dtos);
    // }

    public virtual async Task<ResponseDto<TDto>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if(entity == null)
            return ResponseDto<TDto>.ErrorResult(ErrorCodes.NotFound,"Kayıt bulunamadı.");
        
        var dto = _mapper.Map<TDto>(entity);
        return ResponseDto<TDto>.SuccessResult(dto);
    }

    public async Task<ResponseDto<List<TDto>>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
       var entities = await _repository.GetWhere(predicate).ToListAsync();
        var dtos = _mapper.Map<List<TDto>>(entities);
        return ResponseDto<List<TDto>>.SuccessResult(dtos);
    }

    public async Task<ResponseDto<object>> UpdateAsync(TUpdateDto dto)
    {
        var idProp = dto.GetType().GetProperty("Id");
        if(idProp == null)
            return ResponseDto<object>.ErrorResult(ErrorCodes.BadRequest, "UpdateDto içerisinde 'Id' bulunamadı.");
       
        var id = (int)idProp.GetValue(dto);
        var entity = await _repository.GetByIdAsync(id);
        if(entity == null)
            return ResponseDto<object>.ErrorResult(ErrorCodes.NotFound, "Güncellenicek kayıt bulunamadı.");
        
        _mapper.Map(dto, entity);
        _repository.Update(entity);

        var result = await _repository.SaveChangesAsync();
        if (result > 0)
            return ResponseDto<object>.SuccessResult("Güncelleme başarılı.");

        return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Güncelleme sırasında bir hata oluştu.");

    }
}

