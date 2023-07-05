using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using EmploymentExchange.Repositories;


namespace EmploymentExchange.Middlewares
{
    public class IsAdmin : ActionFilterAttribute
    {
        //private readonly IJWT jwt;
        
        //public IsAdmin(IJWT jwt)
        //{
        //    this.jwt = jwt;
        //}

        //public async override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    string? token = context.HttpContext.Request.Headers.Authorization.ToString();
        //    if (token == null) context.Result = new BadRequestResult();
        //    List<string> roles = await jwt.DecodeJWT(token);
        //    bool admin = roles.Contains("admin");
        //    if (!admin) context.Result = new BadRequestResult();
        //}

    }
}
