using Khouissi_Caisse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Services;

/// <summary>
/// Mock implementation of IUserService for testing and UI development
/// </summary>
public class MockUserService : IUserService
{
    private readonly List<User> _users;    public MockUserService()
    {
        // Initialize with some test users
        _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = "123", // Default password for mock login
                Role = "Admin",
                FirstName = "مدير",
                LastName = "النظام",
                CreatedDate = DateTime.Now.AddYears(-1),
                LastLogin = DateTime.Now.AddDays(-2)
            },
            new User
            {
                Id = 2,
                Username = "user",
                PasswordHash = "123", // Default password for mock login
                Role = "User",
                FirstName = "مستخدم",
                LastName = "عادي",
                CreatedDate = DateTime.Now.AddMonths(-3),
                LastLogin = DateTime.Now.AddDays(-5)
            }
        };
    }

    /// <inheritdoc/>
    public Task<User?> AuthenticateAsync(string username, string password)
    {
        // In a real service, password would be hashed and compared with stored hash
        var user = _users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            u.PasswordHash == password);

        if (user != null)
        {
            // Update last login time for successful authentication
            user.LastLogin = DateTime.Now;
        }

        return Task.FromResult(user);
    }    /// <inheritdoc/>
    public Task<User?> AuthenticateWithPasswordOnlyAsync(string password)
    {
        // Check if the password matches the default password "123"
        if (password == "123")
        {
            // Return the first user (admin) for simplicity
            var user = _users.FirstOrDefault();
            if (user != null)
            {
                // Update last login time for successful authentication
                user.LastLogin = DateTime.Now;
                return Task.FromResult<User?>(user);
            }
        }
        
        // If password doesn't match or no users found
        return Task.FromResult<User?>(null);
    }

    /// <inheritdoc/>
    public Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);

        if (user == null || user.PasswordHash != currentPassword)
        {
            return Task.FromResult(false);
        }

        user.PasswordHash = newPassword; // In real app, would hash the new password
        return Task.FromResult(true);
    }

    /// <inheritdoc/>
    public Task<bool> CreateUserAsync(User user, string password)
    {
        // Check if username already exists
        if (_users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.FromResult(false);
        }

        // Set ID (would be handled by database in real app)
        user.Id = _users.Max(u => u.Id) + 1;
        user.PasswordHash = password; // In real app, would hash the password
        user.CreatedDate = DateTime.Now;

        _users.Add(user);
        return Task.FromResult(true);
    }

    /// <inheritdoc/>
    public Task<User?> GetByIdAsync(int id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    /// <inheritdoc/>
    public Task<User?> GetByUsernameAsync(string username)
    {
        return Task.FromResult(_users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));
    }

    /// <inheritdoc/>
    public Task<bool> UpdateUserAsync(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);

        if (existingUser == null)
        {
            return Task.FromResult(false);
        }

        // Check if trying to change username to one that already exists
        if (!existingUser.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase) &&
            _users.Any(u => u.Id != user.Id && u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.FromResult(false);
        }

        // Update user properties (except password which is handled separately)
        existingUser.Username = user.Username;
        existingUser.Role = user.Role;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;

        return Task.FromResult(true);
    }
}