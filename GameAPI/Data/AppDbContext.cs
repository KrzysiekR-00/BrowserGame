using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterAttribute> CharactersAttributes => Set<CharacterAttribute>();
    public DbSet<ScheduledAttributeChange> ScheduledAttributeChanges => Set<ScheduledAttributeChange>();
}
