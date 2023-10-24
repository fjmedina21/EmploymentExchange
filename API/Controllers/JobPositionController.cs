using API.Models;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("jobpositions")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPosition jobPositionRepo;

        public JobPositionController(IJobPosition jobPositionRepo)
        {
            this.jobPositionRepo = jobPositionRepo;
        }

        [HttpGet]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> GetJobPositions([FromQuery] string? category)
        {
            APIResponse response = await jobPositionRepo.GetAllAsync(category);

            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetJobPositionById([FromRoute] Guid id)
        {
            APIResponse response = await jobPositionRepo.GetByIdAsync(id);

            return response.Ok ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateJobPosition([FromBody] JobPositionDTO jobPosition)
        {
            APIResponse response = await jobPositionRepo.CreateAsync(jobPosition);

            return response.Ok ? CreatedAtAction(nameof(CreateJobPosition), response) : BadRequest(response);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobPosition([FromRoute] Guid id, [FromBody] JobPositionDTO jobPosition)
        {
            APIResponse response = await jobPositionRepo.UpdateAsync(id, jobPosition);

            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteJobPosition([FromRoute] Guid id)
        {
            APIResponse response = await jobPositionRepo.DeleteAsync(id);

            return response.Ok ? NoContent() : NotFound(response);
        }
    }
}
