namespace SporOkulu.Application;

public sealed record UpdateStudentTestDto(
    int Id,
    int StudentId,
    int TestTypeId,
    string Value,
    DateTime TestDate
);
