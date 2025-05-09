using Khouissi_Caisse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> AddAsync(Member member);
        Task<bool> UpdateAsync(Member member);
        Task<bool> DeleteAsync(int id);
        Task<List<Member>> SearchAsync(string searchText);
        Task<List<FamilyRelationship>> GetFamilyMembersAsync(int memberId);
        Task<bool> UpdateFamilyRelationshipsAsync(int memberId, IEnumerable<FamilyRelationship> relationships);
        Task<bool> AddFamilyRelationshipAsync(int memberId, int relatedMemberId, string relationshipType);
        Task<bool> RemoveFamilyRelationshipAsync(int memberId, int relatedMemberId);
    }
}