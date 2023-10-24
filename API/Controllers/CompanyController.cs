using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using API.Helpers;
using API.Repositories;

namespace API.Controllers
{
    [Route("companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany companyRepo;
        private readonly IMapper mapper;

        public CompanyController(ICompany companyRepo, IMapper mapper)
        {
            this.companyRepo = companyRepo;
            this.mapper = mapper;
        }

        //public
        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            APIResponse response = await companyRepo.GetAllAsync(pageNumber, pageSize);

            return Ok(response);
        }

        //public
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] Guid id)
        {
            APIResponse response = await companyRepo.GetByIdAsync(id);

            return response.Ok ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO company)
        {
            APIResponse response = await companyRepo.CreateAsync(company);

            return response.Ok ? CreatedAtAction(nameof(CreateCompany), response) : BadRequest(response);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> UpdateCompany([FromRoute] Guid id, [FromBody] CompanyDTO company)
        {
            APIResponse response = await companyRepo.UpdateAsync(id, company);

            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid id)
        {
            APIResponse response = await companyRepo.DeleteAsync(id);

            return response.Ok ? NoContent() : NotFound(response);
        }

    }
}
