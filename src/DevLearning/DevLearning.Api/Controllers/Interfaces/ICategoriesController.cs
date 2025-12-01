using DevLearning.Api.Models.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers.Interfaces
{
    public interface ICategoriesController
    {
        Task<IActionResult> CreateCategoryAsync(CreateCategoryDTO category);
        Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories();
        Task<ActionResult<CategoryResponseDTO?>> GetCategoryByIdAsync(Guid id);
        Task<IActionResult> UpdateCategoryAsync(Guid id, UpdateCategoryDTO update);
        Task<IActionResult> DeleteCategoryById(Guid id);
    }
}
