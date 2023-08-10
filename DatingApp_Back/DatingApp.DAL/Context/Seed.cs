using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL.Context;

public class Seed
{
    public static async Task SeedUsers(DatingAppContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var userData = await File.ReadAllTextAsync("../DatingApp.DAL/Context/UserSeedData.json");

        var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, option);

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();

            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }
}