using Microsoft.AspNetCore.Mvc;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Helpers;
using EmploymentExchangeAPI.Repositories;
using System.Net;
using Microsoft.AspNetCore.Authorization;

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
            
            if (user is null || token is null) return BadRequest(new APIResponse(StatusCode: 400, Message:"Invalid Credentials"));

            Response.Headers.Authorization = token;
            return Ok(new APIResponse(Data: user));
        }

        [HttpPost]
        [Route("signup")]
        [ValidateModel]
        public Task<IActionResult> Signup()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("change-password")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromHeader()] string authorization, [FromBody] ChangePasswordDTO model)
        {
            APIResponse response = await authRepo.ChangePasswordAsync(model, authorization);

            if (!response.Ok) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        [Route("reset-password")]
        [ValidateModel]
        public Task<IActionResult> ResetPassword()
        {
            throw new NotImplementedException();
        }

        //[HttpPost]
        //[Route("/forgot-password")]
        //public Task<IActionResult> ForgotPassword()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
