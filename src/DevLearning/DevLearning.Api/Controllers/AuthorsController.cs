using DevLearning.Api.Models.Dtos.Author;
using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _authorService;
        private ILogger<AuthorsController> _ilogger;

        public AuthorsController(IAuthorService authorService, ILogger<AuthorsController> ilogger)
        {
            _authorService = authorService;
            _ilogger = ilogger;
        }


        // Get all
        [HttpGet]
        public async Task<ActionResult<List<AuthorResponseDto>>> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorAsync();

                if (authors == null) return NoContent();

                return Ok(authors);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, "Erro inesperado durante a busca dos professores em {time}", DateTime.UtcNow);

                return Problem(ex.Message);
            }
        }


        // Get by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResponseDto>> GetAuthorByIdAsync(Guid id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);

                if (author == null) return NotFound("Professor não encontrado!");

                return Ok(author);
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
                _ilogger.LogError(ex, "Ocorreu um erro inesperado durante a busca do professor");

                return Problem(ex.Message);
            }
        }


        // Post from author
        [HttpPost]
        public async Task<ActionResult> CreateAuthorAsync(CreateAuthorDto author)
        {
            try
            {
                await _authorService.CreateAuthorAsync(author);

                return Created();
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, "Ocorreu um erro inesperado durante o cadastro do professor");

                return Problem(ex.Message);
            }
        }


        // Put from author by ID
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorByIdAsync(UpdateAuthorDto author, Guid id)
        {
            try
            {
                var existing = await _authorService.GetAuthorByIdAsync(id);

                if (existing == null) return NotFound("Professor não encontrado!");

                await _authorService.UpdateAuthorByIdAsync(author, id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, "Ocorreu um erro inesperado durante o update do professor");

                return Problem(ex.Message);
            }
        }


        // Delete from author by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorByIdAsync(Guid id)
        {
            try
            {
                var existing = await _authorService.GetAuthorByIdAsync(id);

                if (existing is null) return NotFound("Professor não encontrado");

                await _authorService.DeleteAuthorByIdAsync(id);

                return NoContent();

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, "Ocorreu um erro inesperado durante o delete do professor");

                return Problem(ex.Message);
            }

        }



    }
}
