using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SporOkulu.Application;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTestResultController : ControllerBase
    {
        private readonly IGenericService<StudentTestResult, DetailStudentTestDto, CreateStudentTestDto,UpdateStudentTestDto> _service;

        public StudentTestResultController(IGenericService<StudentTestResult, DetailStudentTestDto, CreateStudentTestDto, UpdateStudentTestDto> service)
        {
            _service = service;
        }

         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            if(!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if(!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentTestDto dto)
        {
            var response = await _service.AddAsync(dto);
            if(!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentTestDto dto)
        {
            var response = await _service.UpdateAsync(dto);
            if(!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            if(!response.Success) return BadRequest(response);
            return Ok(response);
        }
    
    }
}
