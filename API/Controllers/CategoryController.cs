using API.Models;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("categories")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory categoryRepo;

        public CategoryController(ICategory categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> GetCategories()
        {
            APIResponse response = await categoryRepo.GetCategoriesAsync();

            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            APIResponse response = await categoryRepo.GetCategoryByIdAsync(id);

            return response.Ok ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            APIResponse response = await categoryRepo.CreateCategoryAsync(category);

            return response.Ok ? CreatedAtAction(nameof(CreateCategory), response) : BadRequest(response);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryDTO category)
        {
            APIResponse response = await categoryRepo.UpdateCategoryAsync(id, category);

            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            APIResponse response = await categoryRepo.DeleteCategoryAsync(id);

            return response.Ok ? NoContent() : NotFound(response);
        }
    }
}
