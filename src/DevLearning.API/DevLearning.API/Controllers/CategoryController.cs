using DevLearning.API.Models.DTOs.Category;
using DevLearning.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryRequestDTO categoryDto)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(categoryDto);
                return Created();
            }catch(Exception ex)
            {
                return StatusCode(500, $"Erro interbo: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
