using System;

namespace SporOkulu.Application.DTOs.CoachDTOs;

public sealed record UpdateCoachDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    decimal Salary,
    int BranchId
);
