using Khouissi_Caisse.Models;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Services;

/// <summary>
/// Service interface for user authentication and management
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Authenticates a user with username and password
    /// </summary>
    /// <param name="username">The user's username</param>
    /// <param name="password">The user's password</param>
    /// <returns>The authenticated user or null if authentication fails</returns>
    Task<User?> AuthenticateAsync(string username, string password);

    /// <summary>
    /// Authenticates a user with only a password
    /// </summary>
    /// <param name="password">The password</param>
    /// <returns>The authenticated user or null if authentication fails</returns>
    Task<User?> AuthenticateWithPasswordOnlyAsync(string password);

    /// <summary>
    /// Gets a user by their username
    /// </summary>
    /// <param name="username">The username to search for</param>
    /// <returns>The user or null if not found</returns>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Gets a user by their ID
    /// </summary>
    /// <param name="id">The user ID to search for</param>
    /// <returns>The user or null if not found</returns>
    Task<User?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="password">The plain text password to hash and store</param>
    /// <returns>True if creation was successful</returns>
    Task<bool> CreateUserAsync(User user, string password);

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="user">The user with updated information</param>
    /// <returns>True if update was successful</returns>
    Task<bool> UpdateUserAsync(User user);

    /// <summary>
    /// Changes a user's password
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="currentPassword">The current password for verification</param>
    /// <param name="newPassword">The new password to set</param>
    /// <returns>True if password change was successful</returns>
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
}