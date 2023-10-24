using AutoMapper;
using API.Models;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
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
            APIResponse response = await jobRepo.GetAllAsync(pageNumber, pageSize, filterOn, filterQuery);

            return response.Ok ? Ok(response) : NotFound(response);
        }

        //public
        [HttpGet("{category}")]
        public async Task<IActionResult> GetJobsByCategory([FromRoute] string category, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            APIResponse response = await jobRepo.GetByCategoryAsync(category, pageNumber, pageSize);

            return response.Ok ? Ok(response) : NotFound(response);
        }

        //public
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetJobById([FromRoute] Guid id)
        {
            APIResponse response = await jobRepo.GetByIdAsync(id);

            return response.Ok ? Ok(response) : NotFound(response);
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> CreateJob([FromBody] JobDTO job)
        {
            APIResponse response = await jobRepo.CreateAsync(job);

            return response.Ok ? CreatedAtAction(nameof(CreateJob), response) : BadRequest(response);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobDTO job)
        {
            APIResponse response = await jobRepo.UpdateAsync(id, job);

            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            APIResponse response = await jobRepo.DeleteAsync(id);

            return response.Ok ? NoContent() : NotFound(response);
        }
    }
}
