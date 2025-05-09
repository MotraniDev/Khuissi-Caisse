using Khouissi_Caisse.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Repository for ApplicationSettings entity
/// </summary>
public class ApplicationSettingsRepository : Repository<ApplicationSettings>
{
    /// <summary>
    /// Creates a new application settings repository
    /// </summary>
    /// <param name="context">Database context</param>
    public ApplicationSettingsRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets the ApplicationDbContext
    /// </summary>
    private ApplicationDbContext DbContext => (ApplicationDbContext)_context;

    /// <summary>
    /// Gets the current application settings
    /// </summary>
    /// <returns>Current application settings or null if not configured</returns>
    public async Task<ApplicationSettings?> GetCurrentSettingsAsync()
    {
        // We should only have one settings record, but just in case, return the latest one
        return await DbContext.ApplicationSettings
            .OrderByDescending(s => s.LastModified)
            .FirstOrDefaultAsync();
    }
}
