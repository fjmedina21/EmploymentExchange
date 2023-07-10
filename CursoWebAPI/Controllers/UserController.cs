using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser userRepo;
        private readonly IMapper mapper;

        public UserController(IUser userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        //public
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var (users, total) = await userRepo.GetUsersAsync(pageNumber, pageSize);
            List<GetUserDTO> ReadUsersDTO = mapper.Map<List<GetUserDTO>>(users);

            return Ok(new APIResponse(ReadUsersDTO, total));
        }

        //public
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            User? user = await userRepo.GetUserByIdAsync(id);

            if (user == null) return NotFound(new APIResponse(404, false));

            GetUserDTO ReadUserDTO = mapper.Map<GetUserDTO>(user);

            return Ok(new APIResponse(ReadUserDTO));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            user = await userRepo.CreateUserAsync(user);
            GetUserDTO ReadUserDTO = mapper.Map<GetUserDTO>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new APIResponse(ReadUserDTO, 201));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> Updateuser([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {
            User? user = mapper.Map<User>(userDTO);
            user = await userRepo.UpdateUserAsync(id, user);

            if (user == null) return NotFound(new APIResponse(404, false));

            GetUserDTO ReadUserDTO = mapper.Map<GetUserDTO>(user);

            return Ok(new APIResponse(ReadUserDTO));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deleteuser([FromRoute] Guid id)
        {
            User? user = await userRepo.DeleteUserAsync(id);

            if (user == null) return NotFound(new APIResponse(404, false));

            return NoContent();
        }

    }
}
