using DevLearning.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        //Listar todos os autores
        [HttpGet("Authors")]
        public async Task<ActionResult> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return NotFound($"Lista de autores não encontrada. {ex.Message}");
            }
        }
    }
}
    