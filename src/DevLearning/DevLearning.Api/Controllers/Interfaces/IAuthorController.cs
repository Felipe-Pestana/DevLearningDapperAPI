using DevLearning.Api.Models.Dtos.Author;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface IAuthorController
    {
        Task<ActionResult<List<AuthorResponseDto>>> GetAllAuthorsAsync();

        Task<ActionResult<AuthorResponseDto>> GetAuthorByIdAsync(Guid id);

        Task<ActionResult<AuthorResponseDto>> GetAuthorByEmailAsync(string email);

        Task<ActionResult> CreateAuthorAsync(CreateAuthorDto author);

        Task<ActionResult> UpdateAuthorByIdAsync(UpdateAuthorDto author, Guid id);

        Task<ActionResult> DeleteAuthorByIdAsync(Guid id);
    }
}
