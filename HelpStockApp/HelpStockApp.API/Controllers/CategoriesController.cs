using Microsoft.AspNetCore.Mvc;
using HelpStockApp.Application.DTOs;
using HelpStockApp.Application.Interfaces;

namespace HelpStockApp.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null)
            {
                return NotFound("Categories not found");
            }

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [HttpPost(Name = "CreateCategory")]
        public async Task<ActionResult> Post([FromBody] string name)
        {

            if (name == null)
            {
                return BadRequest("Invalid Body Data");
            }

            var category = new CategoryDTO { Name = name };

            await _categoryService.Add(category);

            return Ok("Created");
        }

        [HttpPut("{id:int}", Name = "UpdateCategory")]
        public async Task<ActionResult> Put(int id, [FromBody] string name)
        {
            if(name == null)    
                return BadRequest("Name is Invalid");

            var category = new CategoryDTO { Name = name, Id = id };

            await _categoryService.Update(category);

            return Ok("Updated");
        }

        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        public async Task<ActionResult> Delete(int? id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            await _categoryService.Remove(id);

            return Ok("Removed");
        }
    }
}
