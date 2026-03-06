namespace SporOkulu.Application;

public sealed record CreateStudentTestDto(
    int StudentId,
    int TestTypeId,
    string Value,
    DateTime TestDate
);
