using DatingApp.BL.Interfaces;
using DatingApp.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.WebApi.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetUserId();
        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await repo.GetUserByIdAsync(userId);
        user.LastActive = DateTime.Now;
        await repo.SaveAllAsync();
    }
}