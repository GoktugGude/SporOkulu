using System;
using AutoMapper;
using SporOkulu.Application.DTOs.AttendanceDTOs;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application.Services;

public class AttendanceManager : IAttendanceService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IMapper _mapper;

    public AttendanceManager(IMapper mapper, IStudentRepository studentRepository, IAttendanceRepository attendanceRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<ResponseDto<List<AttendanceStudentDto>>> GetStudentsForAttendanceAsync(int branchId)
    {
        var students = await _attendanceRepository.GetStudentsByBranchAsync(branchId);
        var dto = _mapper.Map<List<AttendanceStudentDto>>(students);
        return ResponseDto<List<AttendanceStudentDto>>.SuccessResult(dto);
    }

    public async Task SaveAttendanceAsync(List<CreateAttendanceDto> attendanceDtos, DateTime date)
    {
        var attendanceRecords = _mapper.Map<List<Attendance>>(attendanceDtos);
        
        // Her kayda tarihi ekle
        foreach (var record in attendanceRecords)
        {
            record.Date = date;
        }

        await _attendanceRepository.AddRangeAsync(attendanceRecords);
    }
}
