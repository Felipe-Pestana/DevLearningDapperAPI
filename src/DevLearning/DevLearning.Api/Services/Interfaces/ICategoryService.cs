using DevLearning.Api.Models.Dtos.Category;

namespace DevLearning.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryDTO category);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id);
        Task UpdateCategoryById(Guid id, UpdateCategoryDTO update);
        Task DeleteCategoryById(Guid id);
        Task<List<CoursesCategoryDTO>> GetAllCoursesByCategoryIdAsync(Guid categoryId);

    }
}
