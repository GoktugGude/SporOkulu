using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SporOkulu.Application.DTOs.ParentDTOs;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain.Entities;

namespace SporOkulu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _service;

        public ParentController(IParentService service)
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

        [HttpPut]
        public async Task<IActionResult> Update(UpdateParentDto dto)
        {
            var response = await _service.UpdateAsync(dto);
            if(!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
