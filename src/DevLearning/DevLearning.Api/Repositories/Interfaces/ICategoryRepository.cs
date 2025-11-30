using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos;

namespace DevLearning.Api.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id);
        Task DeleteCategoryByIdAsync(Guid id);
        Task UpdateCategoryByIdAsync(Guid id, Category category);
        Task<List<CoursesCategoryDTO>> GetAllCoursesByCategoryIdAsync(Guid categoryId);
    }
}
