using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesController(ILogger<CategoriesController> logger, CategoriesRepository categoriesRepository)
        {
            _logger = logger;
            _categoriesRepository = categoriesRepository;
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            this._categoriesRepository.CreateCategory(category);

            return CreatedAtRoute("GetCategoryById", new { id = category.CategoryID }, category);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public IActionResult GetById(int id)
        {
            var category = this._categoriesRepository.GetCategory(id);

            if (category != null)
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            var categories = this._categoriesRepository.ListCategories();

            return Ok(categories);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCategoryDTO dto)
        {
            var existingCategory = this._categoriesRepository.GetCategory(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            var category = new Category
            {
                CategoryID = id,
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            this._categoriesRepository.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this._categoriesRepository.DeleteCategory(id);

            return NoContent();
        }
    }
}
