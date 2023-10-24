using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using API.Helpers;
using API.Repositories;

namespace API.Controllers
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

            return Ok(new APIResponse(Data: ReadRolesDTO, Total: total));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid id)
        {
            Role? role = await roleRepo.GetRoleByIdAsync(id);

            if (role is null) return NotFound(new APIResponse(StatusCode: 404));

            GetRoleDTO ReadRolesDTO = mapper.Map<GetRoleDTO>(role);

            return Ok(new APIResponse(Data: ReadRolesDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            role = await roleRepo.CreateRoleAsync(role);
            GetRoleDTO ReadRoleDTO = mapper.Map<GetRoleDTO>(role);

            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, new APIResponse(Data: ReadRoleDTO, StatusCode: 201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] RoleDTO roleDTO)
        {
            Role? role = mapper.Map<Role>(roleDTO);
            role = await roleRepo.UpdateRoleAsync(id, role);

            if (role is null) return BadRequest(new APIResponse(StatusCode: 400, Message: "Check that the resource exist and try again"));

            GetRoleDTO ReadRoleDTO = mapper.Map<GetRoleDTO>(role);

            return Ok(new APIResponse(Data: ReadRoleDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            Role? role = await roleRepo.DeleteRoleAsync(id);

            if (role is null) return NotFound(new APIResponse(StatusCode: 404));

            return NoContent();
        }
    }
}
