using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Author;

namespace DevLearning.API.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorResponseDTO>> GetAllAuthorsAsync();
        Task<AuthorResponseDTO> GetAuthorByIdAsync(Guid id);
        Task CreateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Guid id);
        Task UpdateAuthorAsync(Guid id, UpdateAuthorDTO author);
       
    }
}
