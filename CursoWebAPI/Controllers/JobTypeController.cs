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
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> GetJobTypes()
        {
            var (jobTypes, total) = await jobTypeRepo.GetJobTypesAsync();
            List<GetJobTypeDTO> ReadJobTypeDTO = mapper.Map<List<GetJobTypeDTO>>(jobTypes);

            return Ok(new APIResponse(Data:ReadJobTypeDTO, Total:total));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            JobType? jobType = await jobTypeRepo.GetJobTypeByIdAsync(id);

            if (jobType is null) return NotFound(new APIResponse(StatusCode: 404));

            GetJobTypeDTO ReadJobTypeDTO = mapper.Map<GetJobTypeDTO>(jobType);

            return Ok(new APIResponse(Data: ReadJobTypeDTO));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateJobType([FromBody] JobTypeDTO jobTypeDTO)
        {
            JobType jobType = mapper.Map<JobType>(jobTypeDTO);
            jobType = await jobTypeRepo.CreateJobTypeAsync(jobType);
            GetJobTypeDTO ReadJobTypeDTO = mapper.Map<GetJobTypeDTO>(jobType);

            return CreatedAtAction(nameof(GetJobTypeById), new { id = jobType.Id }, new APIResponse(Data:ReadJobTypeDTO, StatusCode: 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobTypeDTO jobTypeDTO)
        {
            JobType? jobType = mapper.Map<JobType>(jobTypeDTO);
            jobType = await jobTypeRepo.UpdateJobTypeAsync(id, jobType);

            if (jobType is null) return BadRequest(new APIResponse(StatusCode: 400));

            GetJobTypeDTO ReadJobTypeDTO = mapper.Map<GetJobTypeDTO>(jobType);

            return Ok(new APIResponse(Data: ReadJobTypeDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            JobType? jobType = await jobTypeRepo.DeleteJobTypeAsync(id);

            if (jobType is null) return BadRequest(new APIResponse(StatusCode: 400));

            return NoContent();
        }
    }
}
