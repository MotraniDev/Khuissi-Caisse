using System;

namespace Khouissi_Caisse.Models;

/// <summary>
/// Represents a user of the system with authentication information
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Username used for authentication
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password hash - never store raw passwords
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Role of the user in the system (e.g., Admin, User)
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// First name of the user
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Date when the user was created
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Last login date
    /// </summary>
    public DateTime? LastLogin { get; set; }
}