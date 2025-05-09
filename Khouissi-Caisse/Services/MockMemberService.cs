using Khouissi_Caisse.Models;
using Khouissi_Caisse.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Services
{
    public class MockMemberService : IMemberService
    {
        private List<Member> _members;
        private List<FamilyRelationship> _relationships;
        private int _nextMemberId = 1;
        private int _nextRelationshipId = 1;

        public MockMemberService()
        {
            _members = new List<Member>
            {
                new Member { Id = _nextMemberId++, CIN = "CIN001", FirstName = "محمد", LastName = "أحمد", BirthDate = new DateTime(1985, 5, 15), Address = "شارع الجزائر رقم 25", Phone = "0555123456", IsActive = true, Notes = "عضو منذ 2018" },
                new Member { Id = _nextMemberId++, CIN = "CIN002", FirstName = "فاطمة", LastName = "محمد", BirthDate = new DateTime(1990, 8, 22), Address = "شارع الأمير عبد القادر رقم 10", Phone = "0555789012", IsActive = true, Notes = "" },
                new Member { Id = _nextMemberId++, CIN = "CIN003", FirstName = "خالد", LastName = "عمر", BirthDate = new DateTime(1975, 1, 3), Address = "حي النصر بناية 5", Phone = "0555345678", IsActive = false, Notes = "غير مشترك منذ 2023" }
            };

            _relationships = new List<FamilyRelationship>();
        }

        public Task<List<Member>> GetAllAsync()
        {
            return Task.FromResult(_members.Select(m => m.Clone()).ToList());
        }

        public Task<Member?> GetByIdAsync(int id)
        {
            var member = _members.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(member?.Clone());
        }

        public Task<Member> AddAsync(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            member.Id = _nextMemberId++;
            var newMemberClone = member.Clone();
            _members.Add(newMemberClone);
            return Task.FromResult(newMemberClone);
        }

        public Task<bool> UpdateAsync(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            var existingMember = _members.FirstOrDefault(m => m.Id == member.Id);
            if (existingMember != null)
            {
                var index = _members.IndexOf(existingMember);
                _members[index] = member.Clone();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var member = _members.FirstOrDefault(m => m.Id == id);
            if (member != null)
            {
                _members.Remove(member);
                _relationships.RemoveAll(r => r.MemberId == id || r.RelatedMemberId == id);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<Member>> SearchAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return Task.FromResult(_members.Select(m => m.Clone()).ToList());

            var lowerSearchText = searchText.ToLower();
            var result = _members
                .Where(m =>
                    (m.FirstName?.ToLower().Contains(lowerSearchText) ?? false) ||
                    (m.LastName?.ToLower().Contains(lowerSearchText) ?? false) ||
                    (m.CIN?.ToLower().Contains(lowerSearchText) ?? false) ||
                    (m.Phone?.Contains(lowerSearchText) ?? false) ||
                    (m.Address?.ToLower().Contains(lowerSearchText) ?? false))
                .Select(m => m.Clone())
                .ToList();
            return Task.FromResult(result);
        }

        public Task<List<FamilyRelationship>> GetFamilyMembersAsync(int memberId)
        {
            var relationships = _relationships
                .Where(r => r.MemberId == memberId)
                .Select(r => r.Clone())
                .ToList();
            return Task.FromResult(relationships);
        }

        public Task<bool> UpdateFamilyRelationshipsAsync(int memberId, IEnumerable<FamilyRelationship> relationships)
        {
            if (relationships == null) return Task.FromResult(false);

            _relationships.RemoveAll(r => r.MemberId == memberId);

            foreach (var relationship in relationships)
            {
                var newRel = relationship.Clone();
                newRel.Id = _nextRelationshipId++;
                newRel.MemberId = memberId;
                _relationships.Add(newRel);
            }
            return Task.FromResult(true);
        }

        public Task<bool> AddFamilyRelationshipAsync(int memberId, int relatedMemberId, string relationshipType)
        {
            if (!_members.Any(m => m.Id == memberId) || !_members.Any(m => m.Id == relatedMemberId) || memberId == relatedMemberId)
                return Task.FromResult(false);

            if (_relationships.Any(r => r.MemberId == memberId && r.RelatedMemberId == relatedMemberId))
                return Task.FromResult(false);

            var relatedMember = _members.First(m => m.Id == relatedMemberId);
            _relationships.Add(new FamilyRelationship
            {
                Id = _nextRelationshipId++,
                MemberId = memberId,
                RelatedMemberId = relatedMemberId,
                RelationshipType = relationshipType,
                RelatedMember = relatedMember.Clone()
            });
            return Task.FromResult(true);
        }

        public Task<bool> RemoveFamilyRelationshipAsync(int memberId, int relatedMemberId)
        {
            var relationship = _relationships.FirstOrDefault(r =>
                r.MemberId == memberId && r.RelatedMemberId == relatedMemberId);

            if (relationship != null)
            {
                _relationships.Remove(relationship);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
