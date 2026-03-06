using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SporOkulu.Application.DTOs.CoachDTOs;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application.Services;

public class CoachManager : GenericManager<Coach, DetailCoachDto, CreateCoachDto, UpdateCoachDto>, ICoachService
{
    private readonly ICoachRepository _coachRepository;
    public CoachManager(IMapper mapper, ICoachRepository coachRepository) : base(coachRepository,mapper)
    {
        _coachRepository = coachRepository;
    }

    // protected override string[] Includes => new [] {"AppUser", "Branch"};

    public override async Task<ResponseDto<List<DetailCoachDto>>> GetAllAsync()
    {
        var data = await _coachRepository.GetCoachesAsync();
        var dto = _mapper.Map<List<DetailCoachDto>>(data);
        return ResponseDto<List<DetailCoachDto>>.SuccessResult(dto);
    }

    public override async Task<ResponseDto<DetailCoachDto>> GetByIdAsync(int id)
    {
        var data = await _coachRepository.GetCoachAsync(id);
        if(data == null) return ResponseDto<DetailCoachDto>.ErrorResult(ErrorCodes.NotFound,"Koç bulunamadı.");
        var dto = _mapper.Map<DetailCoachDto>(data);
        return ResponseDto<DetailCoachDto>.SuccessResult(dto);
    }

}
