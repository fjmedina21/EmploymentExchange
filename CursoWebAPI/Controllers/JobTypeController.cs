using AutoMapper;
using EmploymentExchange.Middlewares;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Controllers
{
    [Route("jobtypes")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetJobTypes()
        {
            List<JobType> jobTypes = await jobTypeRepo.GetJobTypesAsync();
            List<READJobTypeDTO> ReadJobTypeDTO = mapper.Map<List<READJobTypeDTO>>(jobTypes);

            return Ok(new APIResponse(ReadJobTypeDTO));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            JobType? jobType = await jobTypeRepo.GetJobTypeByIdAsync(id);

            if (jobType == null) return NotFound(new APIResponse(404, false));

            READJobTypeDTO ReadJobTypeDTO = mapper.Map<READJobTypeDTO>(jobType);

            return Ok(new APIResponse(ReadJobTypeDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateJobType([FromBody] JobTypeDTO jobTypeDTO)
        {
            JobType jobType = mapper.Map<JobType>(jobTypeDTO);
            jobType = await jobTypeRepo.CreateJobTypeAsync(jobType);
            READJobTypeDTO ReadJobTypeDTO = mapper.Map<READJobTypeDTO>(jobType);

            return CreatedAtAction(nameof(GetJobTypeById), new { id = jobType.Id }, new APIResponse(ReadJobTypeDTO, 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobTypeDTO jobTypeDTO)
        {
            JobType? jobType = mapper.Map<JobType>(jobTypeDTO);
            jobType = await jobTypeRepo.UpdateJobTypeAsync(id, jobType);
            
            if (jobType == null) return NotFound(new APIResponse(404, false));
            
            READJobTypeDTO ReadJobTypeDTO = mapper.Map<READJobTypeDTO>(jobType);

            return Ok(new APIResponse(ReadJobTypeDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            JobType? jobType = await jobTypeRepo.DeleteJobTypeAsync(id);

            if (jobType == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
