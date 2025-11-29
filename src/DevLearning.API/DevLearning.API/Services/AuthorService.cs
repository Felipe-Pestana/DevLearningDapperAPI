using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Author;
using DevLearning.API.Repositories;
using DevLearning.API.Services.Interfaces;

namespace DevLearning.API.Services
{
    public class AuthorService : IAuthorService
    {
        private AuthorRepository _authorRepository;
        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<List<AuthorResponseDTO>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAuthorsAsync();
        }
        public async Task<AuthorResponseDTO> GetAuthorByIdAsync(Guid id)
        {
            return await _authorRepository.GetAuthorByIdAsync(id);
        }

        public async Task CreateAuthorAsync(Author author)
        {
            try
            {
                var findAuthor = await _authorRepository.GetAuthorByEmail(author.Email);
                if (findAuthor is null)
                {
                    var newAuthor = new Author(author.Name, author.Title, author.Image, author.Bio, author.Email, author.Type);
                    await _authorRepository.CreateAuthorAsync(newAuthor);

                }
                else
                {
                    throw new Exception("O autor já existe.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }

        public async Task UpdateAuthorAsync(Guid id, UpdateAuthorDTO author)
        {
            await _authorRepository.UpdateAuthorAsync(id, author);
        }

        //public async Task GetAuthorsCourses()
        //{
        //    // Lógica para pegar os cursos de cada autor será implementada aqui 
        //}
    }
}
