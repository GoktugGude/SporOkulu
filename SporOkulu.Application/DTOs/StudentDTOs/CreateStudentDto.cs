using SporOkulu.Application.DTOs.ParentDTOs;

namespace SporOkulu.Application;

public sealed record CreateStudentDto(
    string FirstName,
    string LastName,
     string? PhoneNumber,
    string? Email,
    DateTime BirthDate,
    // int ParentId,
    int BranchId,
    string? BloodType,
    string? HealthNotes,
    CreateParentDto ParentInfo
    );
