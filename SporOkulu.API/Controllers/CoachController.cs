using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SporOkulu.Application.DTOs.CoachDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain.Entities;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _service;

        public CoachController(ICoachService service)
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
        public async Task<IActionResult> Create(CreateCoachDto dto)
        {
            var response = await _service.AddAsync(dto);
            if(!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCoachDto dto)
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
