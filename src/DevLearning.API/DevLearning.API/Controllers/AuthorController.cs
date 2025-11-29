using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Author;
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

        //Teste de funcionamento da API
        //[HttpGet]
        //public ActionResult HeartBeat()
        //{
        //    return Ok("API is running.");
        //}


        //Listar todos os autores
        [HttpGet]
        public async Task<ActionResult<List<AuthorResponseDTO>>> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = $"Lista de autores não encontrada. {ex.Message}" });
            }
        }

        //Listar autor por Id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthorById(Guid id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = $"Autor não encontrado. {ex.Message}" });
            }
        }

        //Criar autor
        [HttpPost]
        public async Task<ActionResult> CreateAuthor(Author author)
        {
            try
            {
                await _authorService.CreateAuthorAsync(author);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { error = $"Erro ao criar autor. {ex.Message}" });
            }
        }

        //Deletar autor
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
                return Ok("Autor deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(204, new {error = $"Erro ao deletar autor. {ex.Message}" });
            }
           }

        //Atualizar autor
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDTO author)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(id, author);
                return Ok("Autor atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(204, new { error = $"Erro ao atualizar autor. {ex.Message}" });
            }
        }
    }
}
