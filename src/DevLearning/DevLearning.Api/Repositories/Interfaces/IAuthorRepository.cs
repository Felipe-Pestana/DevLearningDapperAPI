using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Author;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorResponseDto>> GetAllAuthorAsync();

        Task CreateAuthorAsync(Author author);

        Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id);

        Task UpdateAuthorByIdAsync(Author author, Guid id);

        Task DeleteAuthorByIdAsync(Guid id);
    }
}
