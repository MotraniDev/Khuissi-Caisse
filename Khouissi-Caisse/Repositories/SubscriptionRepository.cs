using Khouissi_Caisse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Repository for Subscription entity
/// </summary>
public class SubscriptionRepository : Repository<Subscription>
{
    /// <summary>
    /// Creates a new subscription repository
    /// </summary>
    /// <param name="context">Database context</param>
    public SubscriptionRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets the ApplicationDbContext
    /// </summary>
    private ApplicationDbContext DbContext => (ApplicationDbContext)_context;

    /// <summary>
    /// Gets subscriptions for a specific member
    /// </summary>
    /// <param name="memberId">Member ID</param>
    /// <returns>List of subscriptions for the member</returns>
    public async Task<IEnumerable<Subscription>> GetSubscriptionsByMemberAsync(int memberId)
    {
        return await DbContext.Subscriptions
            .Where(s => s.MemberId == memberId)
            .OrderByDescending(s => s.PeriodDate)
            .ToListAsync();
    }

    /// <summary>
    /// Gets subscriptions for a specific period
    /// </summary>
    /// <param name="startDate">Start date (inclusive)</param>
    /// <param name="endDate">End date (inclusive)</param>
    /// <returns>List of subscriptions for the period</returns>
    public async Task<IEnumerable<Subscription>> GetSubscriptionsForPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await DbContext.Subscriptions
            .Where(s => s.PeriodDate >= startDate && s.PeriodDate <= endDate)
            .Include(s => s.Member)
            .OrderByDescending(s => s.PeriodDate)
            .ToListAsync();
    }

    /// <summary>
    /// Checks if a member has paid for a specific month
    /// </summary>
    /// <param name="memberId">Member ID</param>
    /// <param name="year">Year</param>
    /// <param name="month">Month</param>
    /// <returns>True if paid, false otherwise</returns>
    public async Task<bool> HasMemberPaidForMonthAsync(int memberId, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await DbContext.Subscriptions
            .AnyAsync(s => s.MemberId == memberId &&
                          ((s.PeriodDate.Year == year && s.PeriodDate.Month == month) ||
                           (s.IsAdvancePayment && s.PeriodDate <= startDate &&
                            s.PeriodDate.AddMonths(s.MonthsCovered) >= endDate)));
    }
}
