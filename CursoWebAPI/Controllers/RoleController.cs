using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
{
    [Route("roles")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRole roleRepo;
        private readonly IMapper mapper;

        public RoleController(IRole roleRepo, IMapper mapper)
        {
            this.roleRepo = roleRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var (roles, total) = await roleRepo.GetRolesAsync();
            List<GetRoleDTO> ReadRolesDTO = mapper.Map<List<GetRoleDTO>>(roles);

            return Ok(new APIResponse(ReadRolesDTO, total));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid id)
        {
            Role? role = await roleRepo.GetRoleByIdAsync(id);

            if (role == null) return NotFound(new APIResponse(404, false));

            GetRoleDTO ReadRolesDTO = mapper.Map<GetRoleDTO>(role);

            return Ok(new APIResponse(ReadRolesDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            role = await roleRepo.CreateRoleAsync(role);
            GetRoleDTO ReadRoleDTO = mapper.Map<GetRoleDTO>(role);

            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, new APIResponse(ReadRoleDTO, 201));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] RoleDTO roleDTO)
        {
            Role? role = mapper.Map<Role>(roleDTO);
            role = await roleRepo.UpdateRoleAsync(id, role);

            if (role == null) return NotFound(new APIResponse(404, false));

            GetRoleDTO ReadRoleDTO = mapper.Map<GetRoleDTO>(role);

            return Ok(new APIResponse(ReadRoleDTO));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            Role? role = await roleRepo.DeleteRoleAsync(id);

            if (role == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
