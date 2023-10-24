using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using API.Helpers;
using API.Repositories;
using System.Data;

namespace API.Controllers
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
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> GetJobTypes()
        {
            APIResponse response = await jobTypeRepo.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetJobTypeById([FromRoute] Guid id)
        {
            APIResponse response = await jobTypeRepo.GetByIdAsync(id);

            return response.Ok ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateJobType([FromBody] JobTypeDTO jobType)
        {
            APIResponse response = await jobTypeRepo.CreateAsync(jobType);

            return response.Ok ? CreatedAtAction(nameof(CreateJobType), response) : BadRequest(response);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] JobTypeDTO jobType)
        {
            APIResponse response = await jobTypeRepo.UpdateAsync(id, jobType);

            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            APIResponse response = await jobTypeRepo.DeleteAsync(id);

            return response.Ok ? NoContent() : NotFound(response);
        }
    }
}
