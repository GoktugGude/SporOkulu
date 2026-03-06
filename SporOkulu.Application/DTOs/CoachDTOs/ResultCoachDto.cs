using System;

namespace SporOkulu.Application.DTOs.CoachDTOs;

public sealed record DetailCoachDto(
    int Id,
    string FullName,
    string Email,
    string PhoneNumber,
    decimal Salary,
    string BranchName
)
{
    public DetailCoachDto() : this(0, "", "", "", 0, "") { }
}


