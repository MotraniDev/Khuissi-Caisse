using Khouissi_Caisse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Services;

/// <summary>
/// Mock implementation of IMemberService for testing and UI development
/// </summary>
public class MockMemberService : IMemberService
{
    private readonly List<Member> _members;
    private int _nextId = 1;

    /// <summary>
    /// Creates a new instance of MockMemberService with sample data
    /// </summary>
    public MockMemberService()
    {
        // Initialize with some test members
        _members = new List<Member>
        {
            new Member
            {
                Id = _nextId++,
                FirstName = "أحمد",
                LastName = "محمد",
                BirthDate = new DateTime(1980, 5, 15),
                Phone = "0555123456",
                Address = "شارع الاستقلال، حي السلام، الجزائر",
                JoinDate = DateTime.Now.AddMonths(-6),
                IsActive = true
            },
            new Member
            {
                Id = _nextId++,
                FirstName = "فاطمة",
                LastName = "أحمد",
                BirthDate = new DateTime(1985, 8, 22),
                Phone = "0555789012",
                Address = "شارع النصر، حي البدر، الجزائر",
                JoinDate = DateTime.Now.AddMonths(-4),
                IsActive = true
            },
            new Member
            {
                Id = _nextId++,
                FirstName = "محمد",
                LastName = "عبد الله",
                BirthDate = new DateTime(1975, 3, 10),
                Phone = "0555456789",
                Address = "شارع الجزائر، حي النور، الجزائر",
                JoinDate = DateTime.Now.AddMonths(-8),
                IsActive = true
            },
            new Member
            {
                Id = _nextId++,
                FirstName = "خديجة",
                LastName = "علي",
                BirthDate = new DateTime(1990, 11, 5),
                Phone = "0555234567",
                Address = "شارع الثورة، حي الهناء، الجزائر",
                JoinDate = DateTime.Now.AddMonths(-2),
                IsActive = true
            }
        };

        // Create some family relationships
        var parent = _members[0]; // Ahmed
        var child1 = new Member
        {
            Id = _nextId++,
            FirstName = "ياسر",
            LastName = "أحمد",
            BirthDate = new DateTime(2010, 4, 20),
            Phone = "",
            Address = parent.Address,
            JoinDate = DateTime.Now.AddMonths(-6),
            IsActive = true,
            ParentMemberId = parent.Id,
            ParentMember = parent,
            RelationshipToParent = "ابن"
        };
        var child2 = new Member
        {
            Id = _nextId++,
            FirstName = "ليلى",
            LastName = "أحمد",
            BirthDate = new DateTime(2012, 7, 15),
            Phone = "",
            Address = parent.Address,
            JoinDate = DateTime.Now.AddMonths(-6),
            IsActive = true,
            ParentMemberId = parent.Id,
            ParentMember = parent,
            RelationshipToParent = "ابنة"
        };
        _members.Add(child1);
        _members.Add(child2);

        // Initialize the child collections
        parent.ChildMembers = new List<Member> { child1, child2 };
    }

    /// <summary>
    /// Gets all members
    /// </summary>
    public async Task<List<Member>> GetAllAsync()
    {
        // Simulate async operation
        await Task.Delay(100);
        return _members.ToList();
    }

    /// <summary>
    /// Searches for members by name
    /// </summary>
    public async Task<List<Member>> SearchAsync(string searchTerm)
    {
        // Simulate async operation
        await Task.Delay(100);

        if (string.IsNullOrEmpty(searchTerm))
            return _members.ToList();

        return _members
            .Where(m =>
                m.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                m.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                m.Phone.Contains(searchTerm) ||
                m.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Gets a member by ID
    /// </summary>
    public async Task<Member?> GetByIdAsync(int id)
    {
        // Simulate async operation
        await Task.Delay(100);
        return _members.FirstOrDefault(m => m.Id == id);
    }

    /// <summary>
    /// Adds a new member
    /// </summary>
    public async Task<Member> AddAsync(Member member)
    {
        // Simulate async operation
        await Task.Delay(100);

        // Assign an ID
        member.Id = _nextId++;

        // Set default join date if not provided
        if (member.JoinDate == default)
            member.JoinDate = DateTime.Now;

        _members.Add(member);
        return member;
    }

    /// <summary>
    /// Updates an existing member
    /// </summary>
    public async Task<bool> UpdateAsync(Member member)
    {
        // Simulate async operation
        await Task.Delay(100);

        var existingMember = _members.FirstOrDefault(m => m.Id == member.Id);
        if (existingMember == null)
            return false;

        // Update properties
        existingMember.FirstName = member.FirstName;
        existingMember.LastName = member.LastName;
        existingMember.BirthDate = member.BirthDate;
        existingMember.Phone = member.Phone;
        existingMember.Address = member.Address;
        existingMember.Photo = member.Photo;
        existingMember.Notes = member.Notes;
        existingMember.IsActive = member.IsActive;

        return true;
    }

    /// <summary>
    /// Deletes a member
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        // Simulate async operation
        await Task.Delay(100);

        var member = _members.FirstOrDefault(m => m.Id == id);
        if (member == null)
            return false;

        // First check if this member has child members
        var childMembers = _members.Where(m => m.ParentMemberId == id).ToList();
        foreach (var child in childMembers)
        {
            child.ParentMemberId = null;
            child.ParentMember = null;
            child.RelationshipToParent = string.Empty;
        }

        // Remove the member
        _members.Remove(member);
        return true;
    }

    /// <summary>
    /// Gets family members for a parent
    /// </summary>
    public async Task<List<Member>> GetFamilyMembersAsync(int parentId)
    {
        // Simulate async operation
        await Task.Delay(100);

        return _members
            .Where(m => m.ParentMemberId == parentId)
            .ToList();
    }

    /// <summary>
    /// Adds a family relationship
    /// </summary>
    public async Task<bool> AddFamilyRelationshipAsync(int childId, int parentId, string relationship)
    {
        // Simulate async operation
        await Task.Delay(100);

        var child = _members.FirstOrDefault(m => m.Id == childId);
        var parent = _members.FirstOrDefault(m => m.Id == parentId);

        if (child == null || parent == null)
            return false;

        // Set relationship
        child.ParentMemberId = parentId;
        child.ParentMember = parent;
        child.RelationshipToParent = relationship;

        // Add to parent's children collection
        if (parent.ChildMembers == null)
            parent.ChildMembers = new List<Member>();

        if (!parent.ChildMembers.Contains(child))
            parent.ChildMembers.Add(child);

        return true;
    }

    /// <summary>
    /// Removes a family relationship
    /// </summary>
    public async Task<bool> RemoveFamilyRelationshipAsync(int childId)
    {
        // Simulate async operation
        await Task.Delay(100);

        var child = _members.FirstOrDefault(m => m.Id == childId);
        if (child == null || !child.ParentMemberId.HasValue)
            return false;

        var parentId = child.ParentMemberId.Value;
        var parent = _members.FirstOrDefault(m => m.Id == parentId);

        // Remove from parent
        if (parent?.ChildMembers != null)
        {
            parent.ChildMembers.Remove(child);
        }

        // Clear child's parent reference
        child.ParentMemberId = null;
        child.ParentMember = null;
        child.RelationshipToParent = string.Empty;

        return true;
    }
}
