namespace Khouissi_Caisse.Models
{
    public class FamilyRelationship
    {
        public int Id { get; set; } // Added unique ID for the relationship
        public int MemberId { get; set; }
        public int RelatedMemberId { get; set; }
        public string RelationshipType { get; set; } = string.Empty;
        public Member? RelatedMember { get; set; } // Made nullable to align with potential nulls

        public FamilyRelationship Clone()
        {
            return new FamilyRelationship
            {
                Id = this.Id, // Clone the Id
                MemberId = this.MemberId,
                RelatedMemberId = this.RelatedMemberId,
                RelationshipType = this.RelationshipType,
                // Ensure RelatedMember is cloned, assuming Member has a Clone method
                RelatedMember = this.RelatedMember?.Clone() 
            };
        }
    }
}