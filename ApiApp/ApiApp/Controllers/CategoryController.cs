using ApiApp.Data;
using ApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DBData dB;

        public CategoryController(DBData dB)
        {
            this.dB = dB;
        }
        [HttpGet("{id}")] // <-- Added id parameter to the route
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await dB.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found");
            }
            return Ok(category);
        }

        [HttpGet] // <-- No parameters in the route
        public async Task<IActionResult> GetCategories()
        {
            var categories = await dB.categories.ToListAsync();
            if (categories == null || categories.Count == 0)
            {
                return NotFound("Category list is empty");
            }
            return Ok(categories);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                Category category = new Category { Name = categoryName };
                await dB.categories.AddAsync(category);
                await dB.SaveChangesAsync();
                return Ok(category);
            }
            return BadRequest("Category name is required");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Category ID mismatch");
            }

            var existingCategory = await dB.categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found");
            }

            existingCategory.Name = category.Name;
            await dB.SaveChangesAsync();
            return Ok(existingCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await dB.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found");
            }

            dB.categories.Remove(category);
            await dB.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategoryPatch(int id, [FromBody] JsonPatchDocument<Category> categoryPatch)
        {
            var category = await dB.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found");
            }

            categoryPatch.ApplyTo(category, ModelState);
            await dB.SaveChangesAsync();
            return Ok(category);
        }
    }
}
