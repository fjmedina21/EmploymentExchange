using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
{
    [Route("categories")]
    [ApiController]
    [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "poster")]
        public async Task<IActionResult> GetCategories()
        {
            var (categories, total) = await categoryRepo.GetCategoriesAsync();
            List<GetCategoryDTO> ReadCategoryDTO = mapper.Map<List<GetCategoryDTO>>(categories);

            return Ok(new APIResponse(ReadCategoryDTO, total)); ;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            Category? category = await categoryRepo.GetCategoryByIdAsync(id);
            GetCategoryDTO ReadCategoryDTO = mapper.Map<GetCategoryDTO>(category);

            if (category is null) return BadRequest(new APIResponse(400, false));

            return Ok(new APIResponse(ReadCategoryDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            Category category = mapper.Map<Category>(categoryDTO);
            category = await categoryRepo.CreateCategoryAsync(category);
            GetCategoryDTO ReadCategoryDTO = mapper.Map<GetCategoryDTO>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, new APIResponse(ReadCategoryDTO, 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryDTO categoryDTO)
        {
            Category? category = mapper.Map<Category>(categoryDTO);
            category = await categoryRepo.UpdateCategoryAsync(id, category);

            if (category is null) return BadRequest(new APIResponse(400, false));

            GetCategoryDTO ReadCategoryDTO = mapper.Map<GetCategoryDTO>(category);

            return Ok(new APIResponse(ReadCategoryDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            Category? category = await categoryRepo.DeleteCategoryAsync(id);

            if (category is null) return BadRequest(new APIResponse(400, false));

            return NoContent();
        }
    }
}
