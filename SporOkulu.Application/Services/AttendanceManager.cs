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
    private readonly IUnitofWork _uow;
    private readonly IMapper _mapper;


    public AttendanceManager(IUnitofWork uow, IMapper mapper)
    {
        _mapper = mapper;
        _uow = uow;
    }



    public async Task<ResponseDto<List<AttendanceStudentDto>>> GetStudentsForAttendanceAsync(int branchId)
    {
        var students = await _uow.Attendance.GetStudentsByBranchAsync(branchId);
        var dto = _mapper.Map<List<AttendanceStudentDto>>(students);
        return ResponseDto<List<AttendanceStudentDto>>.SuccessResult(dto);
    }

    public async Task<ResponseDto<object>> SaveAttendanceAsync(List<CreateAttendanceDto> attendanceDtos, DateTime date)
    {
        var attendanceRecords = _mapper.Map<List<Attendance>>(attendanceDtos);
        
        // Her kayda tarihi ekle
        foreach (var record in attendanceRecords)
        {
            record.Date = date;
        }

        await _uow.Attendance.AddRangeAsync(attendanceRecords);
        var result = await _uow.SaveChangesAsync();
        if(result > 0) return ResponseDto<object>.SuccessResult(result,"İşlem başarılı");
        return ResponseDto<object>.ErrorResult(ErrorCodes.Exception,"Kaydetme sırasında hata oluştu.");
    }

}
