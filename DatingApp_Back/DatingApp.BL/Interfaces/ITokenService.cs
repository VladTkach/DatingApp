using DatingApp.DAL.Entities;

namespace DatingApp.BL.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}