using AutoMapper;
using EmploymentExchange.Middlewares;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Controllers
{
    [Route("jobs")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IJob jobRepo;
        private readonly IMapper mapper;

        public JobController(IJob jobRepo, IMapper mapper)
        {
            this.jobRepo = jobRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            List<Job> jobs = await jobRepo.GetJobsAsync(pageNumber, pageSize, filterOn, filterQuery);
            List<READJobDTO> ReadJobsDTO = mapper.Map<List<READJobDTO>>(jobs);

            return Ok(new APIResponse(ReadJobsDTO));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetJobById([FromRoute] Guid id)
        {
            Job? job = await jobRepo.GetJobByIdAsync(id);
            
            if (job == null) return NotFound(new APIResponse(404, false));
            
            READJobDTO ReadJobDTO = mapper.Map<READJobDTO>(job);

            return Ok(new APIResponse(ReadJobDTO));
        }

        [HttpGet]
        [Route("{category}")]
        public async Task<IActionResult> GetJobsByCategory([FromRoute] string category, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            List<Job>? jobs = await jobRepo.GetJobsByCategoryAsync(category, pageNumber, pageSize);
            List<READJobDTO> ReadJobsDTO = mapper.Map<List<READJobDTO>>(jobs);

            return Ok(new APIResponse(ReadJobsDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateJob([FromBody] JobDTO jobDTO)
        {
            Job job = mapper.Map<Job>(jobDTO);
            job = await jobRepo.CreateJobAsync(job);
            READJobDTO ReadJobDTO = mapper.Map<READJobDTO>(job);

            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, new APIResponse(ReadJobDTO, 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobDTO jobDTO)
        {
            Job? job = mapper.Map<Job>(jobDTO);
            job = await jobRepo.UpdateJobAsync(id, job);

            if (job == null) return NotFound(new APIResponse(404, false));

            READJobDTO ReadJobDTO = mapper.Map<READJobDTO>(job);

            return Ok(new APIResponse(ReadJobDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            Job? job = await jobRepo.DeleteJobAsync(id);

            if (job == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
