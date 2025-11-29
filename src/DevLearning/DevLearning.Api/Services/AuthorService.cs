using DevLearning.Api.Models.Dtos.Author;
using DevLearning.Api.Repositories.Interfaces;

namespace DevLearning.Api.Services
{
    public class AuthorService
    {
        private IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorResponseDto>> GetAllAuthorAsync()
        {
            return await _authorRepository.GetAllAuthorAsync();
        }


        public async Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id)
        {
            return await _authorRepository.GetAuthorByIdAsync(id);
        }


        public async Task CreateAuthorAsync(CreateAuthorDto author)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateAuthorById(UpdateAuthorDto author, Guid id)
        {
            throw new NotImplementedException();
        }


        public async Task DeleteAuthorById(Guid id)
        {
            await _authorRepository.DeleteAuthorByIdAsync(id);
        }


    }
}
