using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _authorService;
        private ILogger _ilogger;

        public AuthorsController(IAuthorService authorService, ILogger ilogger)
        {
            _authorService = authorService;
            _ilogger = ilogger;
        }


        // Test connection
        [HttpGet]
        public async Task<ActionResult> HeartBeat()
        {
            return Ok("Online");
        }



        // Get all
        [HttpGet("Author")]
        public async Task<ActionResult> GetAllAuthorAsync()
        {
            throw new NotImplementedException();
        }


        // Get by ID
        [HttpGet("Author/{id}")]
        public async Task<ActionResult> GetAuthorByIdAsync(int id)
        {
            throw new NotImplementedException(); 
        }


        // Post from author
        [HttpPost("Author")]
        public async Task<ActionResult> CreateAuthorAsync()
        {
            throw new NotImplementedException();
        }


        // Put from author by ID
        [HttpPut("Author{id}")]
        public async Task<ActionResult> UpdateAuthorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        // Delete from author by ID
        [HttpDelete("Author{id}")]
        public async Task<ActionResult> DeleteAuthorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



    }
}
