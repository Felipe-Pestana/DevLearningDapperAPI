using DevLearning.API.Models.DTOs.Category;

namespace DevLearning.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryRequestDTO categoryDto);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();

    }
}
