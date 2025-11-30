
using DevLearning.Api.Controllers.Interfaces;
using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearning.Api.Services.Interfaces;
using DevLearningAPI.Models.Dtos.Career;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareersController : ControllerBase, ICareerController
    {
        private readonly ICareerService _service;

        public CareersController(ICareerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCareer()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCareerById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCareer([FromBody] CreateCareerDTO dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCareer(Guid id, [FromBody] UpdateCareerDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCareer(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("{id}/items")]
        public async Task<ActionResult> AddItemCareer(Guid id, [FromBody] CreateCareerItemDTO dto)
        {
            try
            {
                await _service.AddItemAsync(id, dto);
                return Created();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{careerId}/items/{courseId}")]
        public async Task<ActionResult> RemoveItemCareer(Guid careerId, Guid courseId)
        {
            try
            {
                await _service.RemoveItemAsync(careerId, courseId);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}