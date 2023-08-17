using DatingApp.Common.DTO.Like;
using DatingApp.Common.DTO.Paged;
using DatingApp.DAL.Entities;

namespace DatingApp.BL.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PagesList<LikeDto>> GetUserLikes(LikesParams likesParams);
}