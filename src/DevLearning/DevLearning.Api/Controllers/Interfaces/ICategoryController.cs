using DevLearning.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICategoryController
    {
        Task<IActionResult> CreateCategoryAsync(CreateCategoryDTO category);
        Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories();
        Task<ActionResult<CategoryResponseDTO?>> GetCategoryByIdAsync(Guid id);
        Task<IActionResult> UpdateCategoryAsync(Guid id, UpdateCategoryDTO update);
        Task<IActionResult> DeleteCategoryById(Guid id);
    }
}
