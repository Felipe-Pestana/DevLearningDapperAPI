using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Category;
using DevLearning.API.Repositories.Interfaces;
using DevLearning.API.Services.Interfaces;

namespace DevLearning.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(CategoryRequestDTO categoryDto)
        {
            var category = new Category(
                categoryDto.Title,
                categoryDto.Url,
                categoryDto.Summary,
                categoryDto.Order,
                categoryDto.Description,
                categoryDto.Featured
            );

            await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return categories ?? new List<CategoryResponseDTO>();
        }
    }
}
