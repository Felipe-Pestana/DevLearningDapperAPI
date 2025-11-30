
using DevLearning.Api.Models.Dtos.CareerItem;
using DevLearningAPI.Models.Dtos.Career;
using DevLearningAPI.Services.Interfaces; 
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareersController : ControllerBase
    {
        private readonly ICareerService _service;

        public CareersController(ICareerService service)
        {
            _service = service;
        }

        [HttpGet("BuscarCarreiras")]
        public async Task<ActionResult> GetAll()
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

        [HttpGet("Buscar Carreira pelo ID{id}")]
        public async Task<ActionResult> GetById(Guid id)
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

        [HttpPost("CriarCarreira")]
        public async Task<ActionResult> Create([FromBody] CreateCareerDTO dto)
        {
            try
            {
                // Ajustado conforme sua interface: CreateAsync recebe apenas o DTO
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

        [HttpPut("AtualizarCarreira{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateCareerDto dto)
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

        [HttpDelete("AlterarSituação{id}")]
        public async Task<ActionResult> Delete(Guid id)
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
        public async Task<ActionResult> AddItem(Guid id, [FromBody] CreateCareerItemDTO dto)
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
    }
}