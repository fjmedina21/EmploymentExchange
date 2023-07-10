using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
{
    [Route("jobpositions")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPosition jobPositionRepo;
        private readonly IMapper mapper;
        private readonly ICategory categoryRepo;

        public JobPositionController(IJobPosition jobPositionRepo, ICategory categoryRepo, IMapper mapper)
        {
            this.jobPositionRepo = jobPositionRepo;
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "poster")]
        public async Task<IActionResult> GetJobPositions([FromQuery] string? category)
        {
            var (jobPositions, total) = await jobPositionRepo.GetJobPositionsAsync(category);
            List<GetJobPositionDTO> ReadJobPositionDTO = mapper.Map<List<GetJobPositionDTO>>(jobPositions);

            return Ok(new APIResponse(ReadJobPositionDTO, total));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetJobPositionById([FromRoute] Guid id)
        {
            JobPosition? jobPosition = await jobPositionRepo.GetJobPositionByIdAsync(id);
            GetJobPositionDTO ReadJobPositionDTO = mapper.Map<GetJobPositionDTO>(jobPosition);

            if (jobPosition == null) return NotFound(new APIResponse(404, false));

            return Ok(ReadJobPositionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateJobPosition([FromBody] JobPositionDTO jobPositionDTO)
        {
            if (await categoryRepo.GetCategoryByIdAsync(jobPositionDTO.CategoryId) == null)
                return BadRequest(new APIResponse(400, false));

            JobPosition jobPosition = mapper.Map<JobPosition>(jobPositionDTO);

            jobPosition = await jobPositionRepo.CreateJobPositionAsync(jobPosition);

            GetJobPositionDTO ReadJobPositionDTO = mapper.Map<GetJobPositionDTO>(jobPosition);

            return CreatedAtAction(nameof(GetJobPositionById), new { id = jobPosition.Id }, new APIResponse(ReadJobPositionDTO, 201));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobPosition([FromRoute] Guid id, [FromBody] JobPositionDTO jobPositionDTO)
        {
            JobPosition? jobPosition = mapper.Map<JobPosition>(jobPositionDTO);
            jobPosition = await jobPositionRepo.UpdateJobPositionAsync(id, jobPosition);

            if (jobPosition == null) return NotFound(new APIResponse(404, false));

            GetJobPositionDTO ReadJobPositionDTO = mapper.Map<GetJobPositionDTO>(jobPosition);

            return Ok(new APIResponse(ReadJobPositionDTO));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteJobPosition([FromRoute] Guid id)
        {
            JobPosition? jobPosition = await jobPositionRepo.DeleteJobPositionAsync(id);

            if (jobPosition == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
