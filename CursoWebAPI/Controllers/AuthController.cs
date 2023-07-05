using EmploymentExchange.Middlewares;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth authRepo;

        public AuthController(IAuth authRepo)
        {
            this.authRepo = authRepo;
        }

        [HttpPost]
        [Route("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            LoggedInDTO? user = await authRepo.LogInAsync(login);

            if (user == null) return BadRequest(new APIResponse(400, false));
            
            return Ok(new APIResponse(user));
        }

        [HttpPost]
        [Route("signup")]
        public Task<IActionResult> Signup()
        {
            throw new NotImplementedException();
        }

        //[HttpPut]
        //[Route("change-password")]
        //public Task<IActionResult> ChangePassword()
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPut]
        //[Route("reset-password")]
        //public Task<IActionResult> ResetPassword()
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //[Route("/forgot-password")]
        //public Task<IActionResult> ForgotPassword()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
