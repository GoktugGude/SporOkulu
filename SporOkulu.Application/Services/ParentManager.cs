using System;
using AutoMapper;
using SporOkulu.Application.DTOs.ParentDTOs;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application.Services;

public class ParentManager : GenericManager<Parent, DetailParentDto, CreateParentDto, UpdateParentDto>, IParentService
{
    private readonly IParentRepository _parentRepository;
    public ParentManager(IParentRepository parentRepository, IMapper mapper) : base(parentRepository, mapper)
    {
        _parentRepository = parentRepository;
    }

     public override async Task<ResponseDto<List<DetailParentDto>>> GetAllAsync()
    {
        var data = await _parentRepository.GetParentsAsync();
        var dto = _mapper.Map<List<DetailParentDto>>(data);
        return ResponseDto<List<DetailParentDto>>.SuccessResult(dto);
    }

    public override async Task<ResponseDto<DetailParentDto>> GetByIdAsync(int id)
    {
        var student = await _parentRepository.GetParentAsync(id);
        if(student == null) return ResponseDto<DetailParentDto>.ErrorResult(ErrorCodes.NotFound, "Veli bulunamadı.");
        var dto = _mapper.Map<DetailParentDto>(student);
        return ResponseDto<DetailParentDto>.SuccessResult(dto);
    }
}