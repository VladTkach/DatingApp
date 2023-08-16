using DatingApp.DAL.Context;
using DatingApp.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[Route("api/[controller]")]
[ApiController]
public class BaseApiController: ControllerBase
{
}