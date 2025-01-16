using AccountWeb.Service.AuthServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountWeb.Core.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        #region Fields
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public AuthFilter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        #endregion
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                var currentUserRoles = await _currentUserService.GetCurrentUserRolesAsync();
                if (currentUserRoles.All(x => x != "User"))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                {
                    await next();

                }
            }
        }
    }
}
