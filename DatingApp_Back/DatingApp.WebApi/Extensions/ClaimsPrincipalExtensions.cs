﻿using System.Security.Claims;

namespace DatingApp.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }
    
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}