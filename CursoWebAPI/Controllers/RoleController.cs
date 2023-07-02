﻿using AutoMapper;
using EmploymentExchange.Middlewares;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Controllers
{
    [Route("roles")]
    [ApiController]
    [Authorize]
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
            List<Role> roles = await roleRepo.GetRolesAsync();
            List<READRoleDTO> ReadRolesDTO = mapper.Map<List<READRoleDTO>>(roles);

            return Ok(new APIResponse(ReadRolesDTO));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid id)
        {
            Role? role = await roleRepo.GetRoleByIdAsync(id);

            if (role == null) return NotFound(new APIResponse(404, false));

            READRoleDTO ReadRolesDTO = mapper.Map<READRoleDTO>(role);

            return Ok(new APIResponse(ReadRolesDTO));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            role = await roleRepo.CreateRoleAsync(role);
            READRoleDTO ReadRoleDTO = mapper.Map<READRoleDTO>(role);

            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, new APIResponse(ReadRoleDTO,201));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] RoleDTO roleDTO)
        {
            Role? role = mapper.Map<Role>(roleDTO);
            role = await roleRepo.UpdateRoleAsync(id, role);

            if (role == null) return NotFound(new APIResponse(404, false));

            READRoleDTO ReadRoleDTO = mapper.Map<READRoleDTO>(role);

            return Ok(new APIResponse(ReadRoleDTO));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            Role? role = await roleRepo.DeleteRoleAsync(id);

            if (role == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }
    }
}
