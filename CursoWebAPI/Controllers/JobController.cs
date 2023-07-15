using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
{
    [Route("jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJob jobRepo;
        private readonly IMapper mapper;

        public JobController(IJob jobRepo, IMapper mapper)
        {
            this.jobRepo = jobRepo;
            this.mapper = mapper;
        }

        //public
        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (jobs, total) = await jobRepo.GetJobsAsync(pageNumber, pageSize, filterOn, filterQuery);
            List<GetJobDTO> ReadJobsDTO = mapper.Map<List<GetJobDTO>>(jobs);

            return Ok(new APIResponse(ReadJobsDTO, total));
        }

        //public
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetJobById([FromRoute] Guid id)
        {
            Job? job = await jobRepo.GetJobByIdAsync(id);

            if (job is null) return NotFound(new APIResponse(404, false));

            GetJobDTO ReadJobDTO = mapper.Map<GetJobDTO>(job);

            return Ok(new APIResponse(ReadJobDTO));
        }

        //public
        [HttpGet]
        [Route("{category}")]
        public async Task<IActionResult> GetJobsByCategory([FromRoute] string category, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            List<Job>? jobs = await jobRepo.GetJobsByCategoryAsync(category, pageNumber, pageSize);
            List<GetJobDTO> ReadJobsDTO = mapper.Map<List<GetJobDTO>>(jobs);

            return Ok(new APIResponse(ReadJobsDTO));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> CreateJob([FromBody] JobDTO jobDTO)
        {
            Job job = mapper.Map<Job>(jobDTO);
            job = await jobRepo.CreateJobAsync(job);
            GetJobDTO ReadJobDTO = mapper.Map<GetJobDTO>(job);

            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, new APIResponse(ReadJobDTO, 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobDTO jobDTO)
        {
            Job? job = mapper.Map<Job>(jobDTO);
            job = await jobRepo.UpdateJobAsync(id, job);

            if (job is null) return NotFound(new APIResponse(404, false));

            GetJobDTO ReadJobDTO = mapper.Map<GetJobDTO>(job);

            return Ok(new APIResponse(ReadJobDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            Job? job = await jobRepo.DeleteJobAsync(id);

            if (job is null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
