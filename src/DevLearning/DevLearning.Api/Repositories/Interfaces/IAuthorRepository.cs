using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Author;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorResponseDto>> GetAllAuthorAsync();

        Task CreateAuthorAsync(Author author);

        Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id);

        Task<AuthorResponseDto> GetAuthorByEmailAsync(string email);

        Task UpdateAuthorByIdAsync(Author author, Guid id);

        Task DeleteAuthorByIdAsync(Guid id);

        Task<Author> GetAuthorCoursesByIdAsync(Guid id);
    }
}
