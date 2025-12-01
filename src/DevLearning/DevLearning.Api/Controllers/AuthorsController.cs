using DevLearning.Api.Controllers.Interfaces;
using DevLearning.Api.Models.Dtos.Author;
using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase, IAuthorsController
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }


        // Get all
        [HttpGet]
        public async Task<ActionResult<List<AuthorResponseDto>>> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorAsync();
                if (authors.IsNullOrEmpty()) return NoContent();

                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while searching for teachers in {time}", DateTime.UtcNow);
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
                return Ok(author);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while searching for the professor in {time}", DateTime.UtcNow);
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during teacher registration in {time}", DateTime.UtcNow);
                return Problem(ex.Message);
            }
        }


        // Put from author by ID
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorByIdAsync(UpdateAuthorDto author, Guid id)
        {
            try
            {
                await _authorService.UpdateAuthorByIdAsync(author, id);
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
                _logger.LogError(ex, "An unexpected error occurred during the professor update in {time}", DateTime.UtcNow);
                return Problem(ex.Message);
            }
        }


        // Delete from author by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorByIdAsync(Guid id)
        {
            try
            {
                await _authorService.DeleteAuthorByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting the professor in {time}", DateTime.UtcNow);
                return Problem(ex.Message);
            }
        }


        // Get Courses by Author
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<AuthorWithCoursesDto>> GetAuthorCoursesAsync(Guid id)
        {
            try
            {
                var result = await _authorService.GetAuthorCoursesAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while searching for the professor's courses in {time}", DateTime.UtcNow);
                return Problem(ex.Message);
            }
        }

    }
}
