using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Category;

namespace DevLearning.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
    }
}
