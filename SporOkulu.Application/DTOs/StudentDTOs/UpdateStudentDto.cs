using SporOkulu.Application.DTOs.ParentDTOs;

namespace SporOkulu.Application;

public sealed record UpdateStudentDto(
    int Id,
    string FirstName,
    string LastName,
     string? PhoneNumber,
    string? Email,
    DateTime BirthDate,
    int ParentId,
    int BranchId, 
    string? BloodType, 
    string? HealthNotes,
    UpdateParentDto ParentDto
 );

