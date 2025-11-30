using DevLearning.Api.Models.Dtos;
using DevLearning.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDTO category)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(category);

                return Created();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDTO?>> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                if (category == null)
                    return NotFound("Categoria não encontrada");

                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(Guid id, UpdateCategoryDTO update)
        {
            try
            {
                await _categoryService.UpdateCategoryById(id, update);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(Guid id)
        {
            try
            {
                await _categoryService.DeleteCategoryById(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("categories/{id})/courses")]
        public async Task<ActionResult<List<CoursesCategoryDTO>>> GetAllCoursesByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var courses = await _categoryService.GetAllCoursesByCategoryIdAsync(categoryId);

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
