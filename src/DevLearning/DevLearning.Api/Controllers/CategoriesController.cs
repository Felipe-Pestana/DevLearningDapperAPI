using DevLearning.Api.Models.Dtos.Category;
using DevLearning.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
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
                    return NotFound("Category not found!");

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

        [HttpGet("{id}/courses")]
        public async Task<ActionResult<List<CoursesCategoryDTO>>> GetAllCoursesByCategoryIdAsync(Guid id)
        {
            try
            {
                var courses = await _categoryService.GetAllCoursesByCategoryIdAsync(id);

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
