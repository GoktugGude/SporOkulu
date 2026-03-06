namespace SporOkulu.Application;

public sealed record CreateAttendanceDto(
    int StudentId, 
    bool IsPresent
);
