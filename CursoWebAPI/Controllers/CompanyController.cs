using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
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
            var (companies, total) = await companyRepo.GetCompaniesAsync(pageNumber, pageSize);
            List<GetCompanyDTO> ReadCompanyDTO = mapper.Map<List<GetCompanyDTO>>(companies);

            return Ok(new APIResponse(ReadCompanyDTO, total));
        }

        //public
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] Guid id)
        {
            Company? company = await companyRepo.GetCompanyByIdAsync(id);
            GetCompanyDTO ReadCompanyDTO = mapper.Map<GetCompanyDTO>(company);

            if (company == null) return NotFound(new APIResponse(404, false));

            return Ok(new APIResponse(ReadCompanyDTO));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO companyDTO)
        {
            Company company = mapper.Map<Company>(companyDTO);
            company = await companyRepo.CreateCompanyAsync(company);
            GetCompanyDTO ReadCompanyDTO = mapper.Map<GetCompanyDTO>(company);

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, new APIResponse(ReadCompanyDTO, 201));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> UpdateCompany([FromRoute] Guid id, [FromBody] CompanyDTO companyDTO)
        {
            Company? company = mapper.Map<Company>(companyDTO);
            company = await companyRepo.UpdateCompanyAsync(id, company);

            if (company == null) return NotFound(new APIResponse(404, false));

            GetCompanyDTO ReadCompanyDTO = mapper.Map<GetCompanyDTO>(company);

            return Ok(new APIResponse(ReadCompanyDTO));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin,poster")]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid id)
        {
            Company? company = await companyRepo.DeleteCompanyAsync(id);

            if (company == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }

    }
}
