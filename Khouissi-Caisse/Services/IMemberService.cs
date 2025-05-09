using Khouissi_Caisse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Services;

/// <summary>
/// Service interface for member management
/// </summary>
public interface IMemberService
{
    /// <summary>
    /// Gets all members
    /// </summary>
    /// <returns>List of all members</returns>
    Task<List<Member>> GetAllAsync();

    /// <summary>
    /// Gets members matching a search term
    /// </summary>
    /// <param name="searchTerm">Term to search for in member names or other fields</param>
    /// <returns>List of matching members</returns>
    Task<List<Member>> SearchAsync(string searchTerm);

    /// <summary>
    /// Gets a member by their ID
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <returns>The member or null if not found</returns>
    Task<Member?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new member
    /// </summary>
    /// <param name="member">Member to add</param>
    /// <returns>The added member with assigned ID</returns>
    Task<Member> AddAsync(Member member);

    /// <summary>
    /// Updates an existing member
    /// </summary>
    /// <param name="member">Member with updated information</param>
    /// <returns>True if update was successful</returns>
    Task<bool> UpdateAsync(Member member);

    /// <summary>
    /// Deletes a member
    /// </summary>
    /// <param name="id">ID of member to delete</param>
    /// <returns>True if deletion was successful</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Gets family members (children) for a parent member
    /// </summary>
    /// <param name="parentId">ID of parent member</param>
    /// <returns>List of family members</returns>
    Task<List<Member>> GetFamilyMembersAsync(int parentId);

    /// <summary>
    /// Adds a family relationship between members
    /// </summary>
    /// <param name="childId">ID of child member</param>
    /// <param name="parentId">ID of parent member</param>
    /// <param name="relationship">Relationship description (e.g., "Son", "Daughter")</param>
    /// <returns>True if relationship was added successfully</returns>
    Task<bool> AddFamilyRelationshipAsync(int childId, int parentId, string relationship);

    /// <summary>
    /// Removes a family relationship
    /// </summary>
    /// <param name="childId">ID of child member</param>
    /// <returns>True if relationship was removed successfully</returns>
    Task<bool> RemoveFamilyRelationshipAsync(int childId);
}
