using DatingApp.DAL.Entities;

namespace DatingApp.BL.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}