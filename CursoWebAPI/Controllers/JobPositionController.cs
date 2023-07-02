using AutoMapper;
using EmploymentExchange.Middlewares;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Controllers
{
    [Route("jobpositions")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetJobPositions([FromQuery] string? category)
        {
            List<JobPosition> jobPositions = await jobPositionRepo.GetJobPositionsAsync(category);
            List<READJobPositionDTO> ReadJobPositionDTO = mapper.Map<List<READJobPositionDTO>>(jobPositions);

            return Ok(new APIResponse(ReadJobPositionDTO));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetJobPositionById([FromRoute] Guid id)
        {
            JobPosition? jobPosition = await jobPositionRepo.GetJobPositionByIdAsync(id);
            READJobPositionDTO ReadJobPositionDTO = mapper.Map<READJobPositionDTO>(jobPosition);

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

            READJobPositionDTO ReadJobPositionDTO = mapper.Map<READJobPositionDTO>(jobPosition);

            return CreatedAtAction(nameof(GetJobPositionById), new { id = jobPosition.Id }, new APIResponse(ReadJobPositionDTO, 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobPosition([FromRoute] Guid id, [FromBody] JobPositionDTO jobPositionDTO)
        {
            JobPosition? jobPosition = mapper.Map<JobPosition>(jobPositionDTO);
            jobPosition = await jobPositionRepo.UpdateJobPositionAsync(id, jobPosition);

            if (jobPosition == null) return NotFound(new APIResponse(404, false));

            READJobPositionDTO ReadJobPositionDTO = mapper.Map<READJobPositionDTO>(jobPosition);

            return Ok(new APIResponse(ReadJobPositionDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteJobPosition([FromRoute] Guid id)
        {
            JobPosition? jobPosition = await jobPositionRepo.DeleteJobPositionAsync(id);

            if (jobPosition == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
