using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SporOkulu.Application.DTOs.CoachDTOs;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Helper;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application.Services;

public class CoachManager : GenericManager<Coach, DetailCoachDto, CreateCoachDto, UpdateCoachDto>, ICoachService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    public CoachManager(IUnitofWork uow, IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService) : base(uow.Coach, mapper, uow)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    // protected override string[] Includes => new [] {"AppUser", "Branch"};

    public override async Task<ResponseDto<List<DetailCoachDto>>> GetAllAsync()
    { 
        var data = await _uow.Coach.GetCoachesAsync();
        var dto = _mapper.Map<List<DetailCoachDto>>(data);
        return ResponseDto<List<DetailCoachDto>>.SuccessResult(dto);
    }

    public override async Task<ResponseDto<DetailCoachDto>> GetByIdAsync(int id)
    {
        var data = await _uow.Coach.GetCoachAsync(id);
        if(data == null) return ResponseDto<DetailCoachDto>.ErrorResult(ErrorCodes.NotFound,"Koç bulunamadı.");
        var dto = _mapper.Map<DetailCoachDto>(data);
        return ResponseDto<DetailCoachDto>.SuccessResult(dto);
    }

    public override async Task<ResponseDto<object>> AddAsync(CreateCoachDto dto)
    {
        var coachPassword = PasswordGenerator.GenerateRandomPassword();
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber
        };

        var identityResult = await _userManager.CreateAsync(user, coachPassword);
        if (!identityResult.Succeeded)
        return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Kullanıcı oluşturulamadı.");
    
        var coach = new Coach
        {
            Salary = dto.Salary,
            BranchId = dto.BranchId,
            AppUserId = user.Id
        };

        await _uow.Coach.AddAsync(coach);
        await _uow.SaveChangesAsync();
        await _userManager.AddToRoleAsync(user, "Coach");
         string subject = "Spor Okulu - Kaydınız Oluşturuldu.";
        string body = $"Merhaba {user.FirstName}, sistem şifreniz: {coachPassword}";
        await _emailService.SendEmailAsync(user.Email,subject,body);
        return ResponseDto<object>.SuccessResult("Koç başarıyla oluşturuldu.");


    }

}
