using System;

namespace SporOkulu.Application.DTOs.ParentDTOs;

public sealed record DetailParentDto(
    int Id,
    string FullName,
    string Email,
    string PhoneNumber
)
{
    public DetailParentDto() : this(0, "", "", "") { }
}
