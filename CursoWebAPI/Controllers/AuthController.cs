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
        private readonly IJWT jwt;

        public AuthController(IAuth authRepo, IJWT jwt)
        {
            this.authRepo = authRepo;
            this.jwt = jwt;
        }

        [HttpPost]
        [Route("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var (user, token) = await authRepo.LoginAsync(login);
            
            if (user is null || token is null) return BadRequest(new APIResponse(Ok:false,StatusCode: 400, Message:"Invalid Credentials"));

            Response.Headers.Authorization = token;
            return Ok(new APIResponse(Data: user));
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
