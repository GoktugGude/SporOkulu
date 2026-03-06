using System;
using SporOkulu.Application.DTOs.AttendanceDTOs;

namespace SporOkulu.Application.Interfaces;


public interface IAttendanceService 
{
    Task<List<AttendanceStudentDto>> GetStudentsForAttendanceAsync(int branchId);
    Task SaveAttendanceAsync(List<CreateAttendanceDto> attendanceDtos, DateTime date);
}

