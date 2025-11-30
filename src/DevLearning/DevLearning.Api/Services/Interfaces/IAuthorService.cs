using DevLearning.Api.Models.Dtos.Author;

namespace DevLearning.Api.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorResponseDto>> GetAllAuthorAsync();

        Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id);

        Task CreateAuthorAsync(CreateAuthorDto author);

        Task UpdateAuthorByIdAsync(UpdateAuthorDto author, Guid id);

        Task DeleteAuthorByIdAsync(Guid id);
    }
}
