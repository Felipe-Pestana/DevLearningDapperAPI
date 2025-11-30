using API.Models.DTOs.Category;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategoriesAsync")]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet("GetCategoryByIdAsync/{id}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryByIdAsync(Guid id)
        {
            return Ok(await _categoryService.GetCategoryByIdAsync(id));
        }

        [HttpPost("CreateCategoryAsync")]
        public async Task<ActionResult> CreateCategoryAsync(CategoryRequestDTO category)
        {
            await _categoryService.CreateCategoryAsync(category);
            return Created();
        }

        [HttpPut("UpdateCategoryAsync/{id}")]
        public async Task<ActionResult> UpdateCategoryAsync(CategoryRequestDTO category, Guid id)
        {
            var categoryFound = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryFound is null)
                return NotFound();

            await _categoryService.UpdateCategoryAsync(category, id);
            return NoContent();
        }

        [HttpDelete("DeleteCategoryAsync/{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(Guid id)
        {
            var categoryFound = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryFound is null)
                return NotFound();

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
