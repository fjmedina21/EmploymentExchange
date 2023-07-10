using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
{
    [Route("jobtypes")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class JobTypeController : ControllerBase
    {
        private readonly IJobType jobTypeRepo;
        private readonly IMapper mapper;

        public JobTypeController(IJobType jobTypeRepo, IMapper mapper)
        {
            this.jobTypeRepo = jobTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "poster")]
        public async Task<IActionResult> GetJobTypes()
        {
            var (jobTypes, total) = await jobTypeRepo.GetJobTypesAsync();
            List<GetJobTypeDTO> ReadJobTypeDTO = mapper.Map<List<GetJobTypeDTO>>(jobTypes);

            return Ok(new APIResponse(ReadJobTypeDTO, total));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            JobType? jobType = await jobTypeRepo.GetJobTypeByIdAsync(id);

            if (jobType == null) return NotFound(new APIResponse(404, false));

            GetJobTypeDTO ReadJobTypeDTO = mapper.Map<GetJobTypeDTO>(jobType);

            return Ok(new APIResponse(ReadJobTypeDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateJobType([FromBody] JobTypeDTO jobTypeDTO)
        {
            JobType jobType = mapper.Map<JobType>(jobTypeDTO);
            jobType = await jobTypeRepo.CreateJobTypeAsync(jobType);
            GetJobTypeDTO ReadJobTypeDTO = mapper.Map<GetJobTypeDTO>(jobType);

            return CreatedAtAction(nameof(GetJobTypeById), new { id = jobType.Id }, new APIResponse(ReadJobTypeDTO, 201));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobTypeDTO jobTypeDTO)
        {
            JobType? jobType = mapper.Map<JobType>(jobTypeDTO);
            jobType = await jobTypeRepo.UpdateJobTypeAsync(id, jobType);

            if (jobType == null) return NotFound(new APIResponse(404, false));

            GetJobTypeDTO ReadJobTypeDTO = mapper.Map<GetJobTypeDTO>(jobType);

            return Ok(new APIResponse(ReadJobTypeDTO));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            JobType? jobType = await jobTypeRepo.DeleteJobTypeAsync(id);

            if (jobType == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
