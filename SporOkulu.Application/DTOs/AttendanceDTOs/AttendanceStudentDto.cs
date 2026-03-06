namespace SporOkulu.Application.DTOs.AttendanceDTOs;

public sealed record AttendanceStudentDto(
    int Id, 
    string FullName
)
{
    public AttendanceStudentDto() : this(0,string.Empty){}

}