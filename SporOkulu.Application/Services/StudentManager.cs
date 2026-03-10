using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Helper;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application.Services;

public class StudentManager : GenericManager<Student, DetailStudentDto, CreateStudentDto, UpdateStudentDto> , IStudentService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IEmailService _emailService;


    public StudentManager(IUnitofWork uow, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService) : base(uow.Student, mapper, uow)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
    }



    public override async Task<ResponseDto<List<DetailStudentDto>>> GetAllAsync()
    {
        var data = await _uow.Student.GetStudentsAsync();
        var dto = _mapper.Map<List<DetailStudentDto>>(data);
        return ResponseDto<List<DetailStudentDto>>.SuccessResult(dto);
    }

    public override async Task<ResponseDto<DetailStudentDto>> GetByIdAsync(int id)
    {
        var student = await _uow.Student.GetStudentAsync(id);
        if(student == null) return ResponseDto<DetailStudentDto>.ErrorResult(ErrorCodes.NotFound, "Öğrenci bulunamadı.");
        var dto = _mapper.Map<DetailStudentDto>(student);
        return ResponseDto<DetailStudentDto>.SuccessResult(dto);
    }

    // public override async Task<ResponseDto<object>> AddAsync(CreateStudentDto dto)
    // {
    //     var student = _mapper.Map<Student>(dto);
    //     // string studentPassword = PasswordGenerator.GenerateRandomPassword();
    //     // string parentPassword = PasswordGenerator.GenerateRandomPassword();
    //     string rawPassword = PasswordGenerator.GenerateRandomPassword();
    //     var appUser = new AppUser{UserName = dto.Email, Email = dto.Email};
    //     var result = await _userManager.CreateAsync(appUser,rawPassword);
    //     if (result.Succeeded)
    // {
    //     // 4. E-posta ile şifreyi gönder
    //     // await _emailService.SendAsync(dto.Email, "Sistem Şifreniz", rawPassword);
    //     return ResponseDto<object>.SuccessResult("Kayıt başarılı.");
    // }
    // return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, result.Errors.FirstOrDefault()?.Description);
    // }
    //    public override async Task<ResponseDto<object>> AddAsync(CreateStudentDto dto)
    // {
    //     string parentPassword = PasswordGenerator.GenerateRandomPassword();
    //     string studentPassword = PasswordGenerator.GenerateRandomPassword();
    //     // 1. Önce Veli'yi oluştur (Identity ile)
    //     var parentUser = new AppUser { Email = dto.ParentInfo.Email, UserName = dto.ParentInfo.Email };
    //    var pResult = await _userManager.CreateAsync(parentUser, parentPassword);

    // if (!pResult.Succeeded) 
    // {
    //     // Hatanın nedenini al (Örn: Şifre çok basit, email zaten kayıtlı vb.)
    //     var errors = string.Join(", ", pResult.Errors.Select(e => e.Description));
    //     return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, $"Veli oluşturulamadı: {errors}");
    // }

    //     await _userManager.AddToRoleAsync(parentUser, "Parent");

    //     // 2. Öğrenci'yi oluştur (Identity ile)
    //     var studentUser = new AppUser { Email = dto.Email, UserName = dto.Email };
    //     var studentResult = await _userManager.CreateAsync(studentUser, studentPassword);

    //     if (!studentResult.Succeeded)
    //         return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Öğrenci oluşturulamadı.");

    //     await _userManager.AddToRoleAsync(studentUser, "Student");

    //     // 3. Veritabanı Entity'lerini eşle
    //     var studentEntity = _mapper.Map<Student>(dto);
    //     studentEntity.AppUserId = studentUser.Id;
    //     studentEntity.Parent = new Parent { AppUserId = parentUser.Id };

    //     // 4. Repository ile kaydet
    //     await _studentRepository.AddAsync(studentEntity);
    //     var result = await _studentRepository.SaveChangesAsync();

    //     if (result > 0)
    //         return ResponseDto<object>.SuccessResult("Kayıt başarıyla gerçekleşti.");

    //     return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Veritabanına kaydedilemedi.");
    // }

    public override async Task<ResponseDto<object>> AddAsync(CreateStudentDto dto)
    {
        // AutoMapper ile nesne oluşturuldu
            var student = _mapper.Map<Student>(dto);
            string studentPassword = PasswordGenerator.GenerateRandomPassword();
            string parentPassword = PasswordGenerator.GenerateRandomPassword();
            // Öğrenci için hashleme
            student.AppUser.PasswordHash = _userManager.PasswordHasher.HashPassword(student.AppUser, studentPassword);

            student.AppUser.NormalizedUserName = _userManager.NormalizeName(student.AppUser.UserName);
            student.AppUser.NormalizedEmail = _userManager.NormalizeEmail(student.AppUser.Email);
            student.AppUser.SecurityStamp = Guid.NewGuid().ToString();
            // Veli için hashleme
            student.Parent.AppUser.PasswordHash = _userManager.PasswordHasher.HashPassword(student.Parent.AppUser, parentPassword);

            student.Parent.AppUser.NormalizedUserName = _userManager.NormalizeName(student.Parent.AppUser.UserName);
            student.Parent.AppUser.NormalizedEmail = _userManager.NormalizeEmail(student.Parent.AppUser.Email);
            student.Parent.AppUser.SecurityStamp = Guid.NewGuid().ToString();
       
            // Ardından repo ile ekle ve kaydet
            await _uow.Student.AddAsync(student);
           var result = await _uow.SaveChangesAsync();
     await _userManager.AddToRoleAsync(student.AppUser, "Student");
        await _userManager.AddToRoleAsync(student.Parent.AppUser, "Parent");
           
            string subject = "Spor Okulu - Kaydınız Oluşturuldu.";
        string studentBody = $"Merhaba {student.AppUser.FirstName}, sistem şifreniz: {studentPassword}";
        string parentBody = $"Merhaba {student.AppUser.FirstName}, sistem şifreniz: {parentPassword}";
        await _emailService.SendEmailAsync(student.AppUser.Email,subject,studentBody);
        await _emailService.SendEmailAsync(student.Parent.AppUser.Email,subject,parentBody);
           if(result > 0) 
              return ResponseDto<object>.SuccessResult("Kayıt başarıyla eklendi.");
        return ResponseDto<object>.ErrorResult(ErrorCodes.Exception, "Kayıt sırasında bir hata oluştu");
    }
}


