using Microsoft.AspNetCore.Mvc;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;

namespace EmploymentExchangeAPI.Controllers
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
            var (user, token) = await authRepo.LogInAsync(login);

            if (user == null) return BadRequest(new APIResponse(400, false));

            Response.Headers.Authorization = token;
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
