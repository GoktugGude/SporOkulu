using System;
using SporOkulu.Application.DTOs.AttendanceDTOs;
using SporOkulu.Application.DTOs.ResponseDTOs;

namespace SporOkulu.Application.Interfaces;


public interface IAttendanceService 
{
    Task<ResponseDto<List<AttendanceStudentDto>>> GetStudentsForAttendanceAsync(int branchId);
    Task SaveAttendanceAsync(List<CreateAttendanceDto> attendanceDtos, DateTime date);
}

