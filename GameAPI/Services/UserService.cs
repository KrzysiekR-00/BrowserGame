using GameAPI.Data;
using GameAPI.Models;

namespace GameAPI.Services;

public class UserService
{
    private readonly AppDbContext _db;
    private readonly string _pepper;

    public UserService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _pepper = config["Security:Pepper"]!;
    }

    public bool UserExists(string username)
    {
        return _db.Users.Any(u => u.Username.ToLower() == username.ToLower());
    }

    public User? GetByUsername(string username)
    {
        return _db.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
    }

    public bool VerifyPassword(User user, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password + _pepper, user.PasswordHash);
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password + _pepper);
    }

    public void AddUser(string username, string password)
    {
        var hash = HashPassword(password);

        var user = new User
        {
            Username = username,
            PasswordHash = hash,
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public User[] GetUsers() => _db.Users.ToArray();
}
