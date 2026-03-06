using System.Security.Cryptography.X509Certificates;
using SporOkulu.Application.DTOs.ParentDTOs;

namespace SporOkulu.Application;

public sealed record DetailStudentDto(
    int Id,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string? Email,
    DateTime BirthDate,
    DetailParentDto Parent,
    string BranchName,
    string BloodType,
    string HealthNotes, 
    List<DetailPaymentDto> Payments,
    List<DetailStudentTestDto> TestResults
    // List<> Attendances
    )
    {
public DetailStudentDto() : this(0, string.Empty,string.Empty, null, null, DateTime.Now, null!, "", "", "", new(), new()) { }
}
