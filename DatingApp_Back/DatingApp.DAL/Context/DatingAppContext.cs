using DatingApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL.Context;

public class DatingAppContext: DbContext
{
    public DatingAppContext(DbContextOptions<DatingAppContext> options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
}