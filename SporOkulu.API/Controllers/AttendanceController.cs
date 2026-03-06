using Microsoft.AspNetCore.Mvc;
using SporOkulu.Application.Interfaces;
using SporOkulu.Application.DTOs;
using SporOkulu.Application;

namespace SporOkulu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    // 1. Branşa göre öğrencileri getir (Yoklama ekranı için)
    [HttpGet("branch/{branchId}")]
    public async Task<IActionResult> GetStudents(int branchId)
    {
        var result = await _attendanceService.GetStudentsForAttendanceAsync(branchId);
        return Ok(result);
    }

    // 2. Yoklamayı kaydet
    [HttpPost("save/{date}")]
    public async Task<IActionResult> Save([FromBody] List<CreateAttendanceDto> attendanceDtos, DateTime date)
    {
        await _attendanceService.SaveAttendanceAsync(attendanceDtos, date);
        return Ok(new { Message = "Yoklama başarıyla kaydedildi." });
    }
}