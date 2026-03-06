using System;

namespace SporOkulu.Application.DTOs.ParentDTOs;

public sealed record CreateParentDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
