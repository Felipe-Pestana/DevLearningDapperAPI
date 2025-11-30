using API.Models.DTOs.Career;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CareerController : ControllerBase
    {
        private readonly ICareerService _service;
        public CareerController(ICareerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCareerAsync()
        {
            var careers = await _service.GetAllCareerAsync();
            return Ok(careers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCareerByIdAsync(Guid id)
        {
            var career = await _service.GetCareerByIdAsync(id);

            if (career == null)
            {
                return NotFound();
            }
            return Ok(career);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCareerAsync([FromBody] CareerRequestDTO career)
        {
            var newCareerId = await _service.CreateCareerAsync(career);
            return CreatedAtAction(nameof(GetCareerByIdAsync), new { id = newCareerId }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCareerAsync(Guid id, [FromBody] CareerRequestDTO career)
        {
            var updated = await _service.UpdateCareerAsync(id, career);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
