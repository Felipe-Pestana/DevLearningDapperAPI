using DevLearning.Api.Models.Dtos;

namespace DevLearning.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryDTO category);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id);
        Task UpdateCategoryById(Guid id, UpdateCategoryDTO update);
        Task DeleteCategoryById(Guid id);

    }
}
