using System;
using System.Collections.Generic;

namespace Khouissi_Caisse.Models;

/// <summary>
/// Represents a member of the subscription fund
/// </summary>
public class Member
{
    /// <summary>
    /// Unique identifier for the member
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// First name of the member (in Arabic)
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the member (in Arabic)
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the member (convenience property)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Address (in Arabic)
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Photo of the member (stored as byte array)
    /// </summary>
    public byte[]? Photo { get; set; }

    /// <summary>
    /// Date when the member joined
    /// </summary>
    public DateTime JoinDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Optional notes about the member
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if the member is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Reference to parent member (for family relationships)
    /// </summary>
    public int? ParentMemberId { get; set; }

    /// <summary>
    /// Parent member (for family relationships) - navigation property
    /// </summary>
    public Member? ParentMember { get; set; }

    /// <summary>
    /// Child members (for family relationships) - navigation property
    /// </summary>
    public List<Member>? ChildMembers { get; set; }

    /// <summary>
    /// Relationship to parent member (e.g., "Son", "Daughter", "Spouse")
    /// </summary>
    public string RelationshipToParent { get; set; } = string.Empty;

    /// <summary>
    /// Path to the photo of the member
    /// </summary>
    public string PhotoPath { get; set; } = string.Empty;

    /// <summary>
    /// National Identification Number (CIN) of the member
    /// </summary>
    public string CIN { get; set; } = string.Empty;

    /// <summary>
    /// Creates a copy of the current member
    /// </summary>
    /// <returns>A new member object with the same properties</returns>
    public Member Clone()
    {
        return new Member
        {
            Id = this.Id,
            FirstName = this.FirstName,
            LastName = this.LastName,
            BirthDate = this.BirthDate,
            Phone = this.Phone,
            Address = this.Address,
            PhotoPath = this.PhotoPath,
            IsActive = this.IsActive,
            Notes = this.Notes
        };
    }
}
