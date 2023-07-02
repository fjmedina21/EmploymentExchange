using AutoMapper;
using EmploymentExchange.Middlewares;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Controllers
{
    [Route("categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory categoryRepo;
        private readonly IMapper mapper;

        public CategoryController(ICategory categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            List<Category> categories = await categoryRepo.GetCategoriesAsync();
            List<READCategoryDTO> ReadCategoryDTO = mapper.Map<List<READCategoryDTO>>(categories);

            return Ok(new APIResponse(ReadCategoryDTO)); ;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            Category? category = await categoryRepo.GetCategoryByIdAsync(id);
            READCategoryDTO ReadCategoryDTO = mapper.Map<READCategoryDTO>(category);

            if (category == null) return NotFound(new APIResponse(404, false));

            return Ok(new APIResponse(ReadCategoryDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            Category category = mapper.Map<Category>(categoryDTO);
            category = await categoryRepo.CreateCategoryAsync(category);
            READCategoryDTO ReadCategoryDTO = mapper.Map<READCategoryDTO>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, new APIResponse(ReadCategoryDTO, 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryDTO categoryDTO)
        {
            Category? category = mapper.Map<Category>(categoryDTO);
            category = await categoryRepo.UpdateCategoryAsync(id, category);

            if (category == null) return NotFound(new APIResponse(404, false));

            READCategoryDTO ReadCategoryDTO = mapper.Map<READCategoryDTO>(category);

            return Ok(new APIResponse(ReadCategoryDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            Category? category = await categoryRepo.DeleteCategoryAsync(id);

            if (category == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
