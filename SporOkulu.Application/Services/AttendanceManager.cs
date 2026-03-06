using System;
using AutoMapper;
using SporOkulu.Application.DTOs.AttendanceDTOs;
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

    public async Task<List<AttendanceStudentDto>> GetStudentsForAttendanceAsync(int branchId)
    {
        var students = await _attendanceRepository.GetStudentsByBranchAsync(branchId);
        return _mapper.Map<List<AttendanceStudentDto>>(students);
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
