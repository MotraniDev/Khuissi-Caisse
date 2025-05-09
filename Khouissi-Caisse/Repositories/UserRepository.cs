using Khouissi_Caisse.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Repository for User entity
/// </summary>
public class UserRepository : Repository<User>
{
    /// <summary>
    /// Creates a new user repository
    /// </summary>
    /// <param name="context">Database context</param>
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets the ApplicationDbContext
    /// </summary>
    private ApplicationDbContext DbContext => (ApplicationDbContext)_context;

    /// <summary>
    /// Gets a user by username
    /// </summary>
    /// <param name="username">Username to find</param>
    /// <returns>User with the matching username or null if not found</returns>
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await DbContext.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }

    /// <summary>
    /// Checks if a username exists
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <returns>True if the username exists, false otherwise</returns>
    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await DbContext.Users
            .AnyAsync(u => u.Username.ToLower() == username.ToLower());
    }
}
