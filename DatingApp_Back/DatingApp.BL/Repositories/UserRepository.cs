using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.BL.Interfaces;
using DatingApp.Common.DTO.User;
using DatingApp.DAL.Context;
using DatingApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.BL.Repositories;

public class UserRepository: IUserRepository
{
    private readonly DatingAppContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DatingAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Photos)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Photos)
            .SingleOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MemberDto> GetMemberAsync(string username)
    {
        return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
}