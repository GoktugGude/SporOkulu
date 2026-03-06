using System.Runtime.InteropServices;
using AutoMapper;
using SporOkulu.Application.DTOs.AttendanceDTOs;
using SporOkulu.Application.DTOs.CoachDTOs;
using SporOkulu.Application.DTOs.ParentDTOs;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
       // --- Student Mapping ---
// Create: DTO -> Entity (Kaydederken)
CreateMap<CreateStudentDto, Student>()
    .ForPath(dest => dest.AppUser.FirstName, opt => opt.MapFrom(src => src.FirstName))
    .ForPath(dest => dest.AppUser.LastName, opt => opt.MapFrom(src => src.LastName))
    .ForPath(dest => dest.AppUser.Email, opt => opt.MapFrom(src => src.Email))
    .ForPath(dest => dest.AppUser.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
    .ForPath(dest => dest.AppUser.UserName, opt => opt.MapFrom(src => src.Email))// Login için Email = UserName
    .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.ParentInfo));


// Read: Entity -> Detail DTO (Listelerken)
CreateMap<Student, DetailStudentDto>()
     .ForMember(to => to.FirstName, from => from.MapFrom(from => from.AppUser.FirstName))
     .ForMember(to => to.LastName, from => from.MapFrom(from => from.AppUser.LastName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
    .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
    .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
    .ForMember(to => to.Payments, from => from.MapFrom(from => from.Payments))
    .ForMember(to => to.TestResults, from => from.MapFrom(from => from.TestResults));


// Update: DTO -> Entity (Güncellerken)
CreateMap<UpdateStudentDto, Student>()
    .ForPath(dest => dest.AppUser.FirstName, opt => opt.MapFrom(src => src.FirstName))
    .ForPath(dest => dest.AppUser.LastName, opt => opt.MapFrom(src => src.LastName));

// --- Parent Mapping ---
CreateMap<CreateParentDto, Parent>()
    .ForPath(dest => dest.AppUser.FirstName, opt => opt.MapFrom(src => src.FirstName))
    .ForPath(dest => dest.AppUser.LastName, opt => opt.MapFrom(src => src.LastName))
    .ForPath(dest => dest.AppUser.Email, opt => opt.MapFrom(src => src.Email))
    .ForPath(dest => dest.AppUser.UserName, opt => opt.MapFrom(src => src.Email));

CreateMap<Parent, DetailParentDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.AppUser.FirstName} {src.AppUser.LastName}"))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber));

// --- Coach Mapping ---
CreateMap<CreateCoachDto, Coach>()
    .ForPath(dest => dest.AppUser.FirstName, opt => opt.MapFrom(src => src.FirstName))
    .ForPath(dest => dest.AppUser.LastName, opt => opt.MapFrom(src => src.LastName))
    .ForPath(dest => dest.AppUser.Email, opt => opt.MapFrom(src => src.Email))
    .ForPath(dest => dest.AppUser.UserName, opt => opt.MapFrom(src => src.Email))
    .ForPath(dest => dest.AppUser.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

CreateMap<Coach, DetailCoachDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.AppUser.FirstName} {src.AppUser.LastName}"))
    .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
    .ForMember(dest => dest.Email, from => from.MapFrom(from => from.AppUser.Email))
    .ForMember(to => to.PhoneNumber, from => from.MapFrom(from => from.AppUser.PhoneNumber));

        //Attendance
    CreateMap<Student, AttendanceStudentDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.AppUser.FirstName} {src.AppUser.LastName}"));
    CreateMap<CreateAttendanceDto, Attendance>().ReverseMap();

        //Branch
        CreateMap<Branch, CreateBranchDto>().ReverseMap();
        CreateMap<Branch,UpdateBranchDto>().ReverseMap();
        CreateMap<Branch, DetailBranchDto>().ReverseMap();

        //Paymnet
        CreateMap<Payment, CreatePaymentDto>().ReverseMap();
        CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        CreateMap<Payment, DetailPaymentDto>().
            ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.AppUser.FirstName} {src.Student.AppUser.LastName}"));

        //StudentTest
        CreateMap<StudentTestResult, CreateStudentTestDto>().ReverseMap(); 
        CreateMap<StudentTestResult, UpdateStudentTestDto>().ReverseMap(); 
        CreateMap<StudentTestResult, DetailStudentTestDto>()
        .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => $"{src.Student.AppUser.FirstName} {src.Student.AppUser.LastName}"));
          

        //TestType
        CreateMap<TestType, CreateTestTypeDto>().ReverseMap(); 
        CreateMap<TestType, UpdateTestTypeDto>().ReverseMap(); 
        CreateMap<TestType, DetailTestTypeDto>().ReverseMap(); 

    }
}
