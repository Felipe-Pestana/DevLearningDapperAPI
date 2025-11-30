using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Category;
using DevLearning.Api.Repositories;
using DevLearning.Api.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace DevLearning.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        //private readonly CourseRepository _courseRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //public CourseService(CourseRepository courseRepository)
        //{
        //    _courseRepository = courseRepository;
        //}

        public async Task CreateCategoryAsync(CreateCategoryDTO category)
        {
            try
            {
                var newCategory = new Category(
                    category.Title,
                        category.Url,
                        category.Summary,
                        category.Order,
                        category.Description,
                        category.Featured);
                await _categoryRepository.CreateCategoryAsync(newCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetAllCategoriesAsync();
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                return await _categoryRepository.GetCategoryByIdAsync(id);
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCategoryById(Guid id, UpdateCategoryDTO update)
        {
            try
            {
                var oldCategory = await _categoryRepository.GetCategoryByIdAsync(id) ??
                    throw new KeyNotFoundException("Categoria não encontrada!");

                var updateCategory = new Category(
                    oldCategory.Title,
                    oldCategory.Url,
                    update.Summary ?? oldCategory.Summary,
                    update.Order ?? oldCategory.Order,
                    update.Description ?? oldCategory.Description,
                    update.Featured ?? oldCategory.Featured);

                await _categoryRepository.UpdateCategoryByIdAsync(id, updateCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id) ??
                   throw new KeyNotFoundException("Categoria não encontrada!");

                //await _courseRepository.DeleteCourseByCategoryAsync(id);

                await _categoryRepository.DeleteCategoryByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CoursesCategoryDTO>> GetAllCoursesByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId) ??
                   throw new KeyNotFoundException("Categoria não encontrada!");

                var coursesWithCategory = await _categoryRepository.GetAllCoursesByCategoryIdAsync(categoryId) ??
                    throw new KeyNotFoundException("Não há cursos com essa categoria!");

                return coursesWithCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
