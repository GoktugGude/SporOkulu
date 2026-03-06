namespace SporOkulu.Application;

public sealed record DetailStudentTestDto(
    int Id,
    string StudentName,
    string TestTypeName,
    string TestTypeUnit,
    string Value,
    DateTime TestDate
)
{
public DetailStudentTestDto() : this(0, string.Empty, "","", string.Empty, DateTime.Now) { }
}
