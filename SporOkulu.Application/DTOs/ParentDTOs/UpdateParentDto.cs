using System;

namespace SporOkulu.Application.DTOs.ParentDTOs;

public sealed record UpdateParentDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
