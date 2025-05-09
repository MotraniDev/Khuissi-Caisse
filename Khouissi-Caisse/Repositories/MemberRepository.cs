using Khouissi_Caisse.Models;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Repository for Member entity
/// </summary>
public class MemberRepository : Repository<Member>
{
    /// <summary>
    /// Creates a new member repository
    /// </summary>
    /// <param name="context">Database context</param>
    public MemberRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets the ApplicationDbContext
    /// </summary>
    private ApplicationDbContext DbContext => (ApplicationDbContext)_context;
}
