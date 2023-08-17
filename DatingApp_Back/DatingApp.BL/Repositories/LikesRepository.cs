using AutoMapper;
using DatingApp.BL.Interfaces;
using DatingApp.Common.DTO.Like;
using DatingApp.Common.DTO.Paged;
using DatingApp.DAL.Context;
using DatingApp.DAL.Entities;
using DatingApp.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.BL.Repositories;

public class LikesRepository : ILikesRepository
{
    private readonly DatingAppContext _context;
    private readonly IMapper _mapper;

    public LikesRepository(DatingAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
    {
        return await _context.Likes.FindAsync(sourceUserId, targetUserId);
    }

    public async Task<AppUser> GetUserWithLikes(int userId)
    {
        return await _context.Users
            .Include(x => x.LikedUsers)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<PagesList<LikeDto>> GetUserLikes(LikesParams likesParams)
    {
        var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
        var likes = _context.Likes.AsQueryable();

        if (likesParams.Predicate == "liked")
        {
            likes = likes.Where(likes => likes.SourceUserId == likesParams.UserId);
            users = likes.Select(like => like.TargetUser);
        }
        
        if (likesParams.Predicate == "likedBy")
        {
            likes = likes.Where(likes => likes.TargetUserId == likesParams.UserId);
            users = likes.Select(like => like.SourceUser);
        }

        var likedUsers = users.Select(user => new LikeDto
        {
            UserName = user.UserName,
            KnownAs = user.KnownAs,
            Age = user.DateOfBirth.CalculateAge(),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)!.Url,
            City = user.City,
            Id = user.Id
        });

        return await PagesList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
    }
}