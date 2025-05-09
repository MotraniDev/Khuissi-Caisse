using Khouissi_Caisse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Repository for Expense entity
/// </summary>
public class ExpenseRepository : Repository<Expense>
{
    /// <summary>
    /// Creates a new expense repository
    /// </summary>
    /// <param name="context">Database context</param>
    public ExpenseRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets the ApplicationDbContext
    /// </summary>
    private ApplicationDbContext DbContext => (ApplicationDbContext)_context;

    /// <summary>
    /// Gets expenses for a specific period
    /// </summary>
    /// <param name="startDate">Start date (inclusive)</param>
    /// <param name="endDate">End date (inclusive)</param>
    /// <returns>List of expenses for the period</returns>
    public async Task<IEnumerable<Expense>> GetExpensesForPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await DbContext.Expenses
            .Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate)
            .Include(e => e.RecordedByUser)
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
    }

    /// <summary>
    /// Gets expenses by category
    /// </summary>
    /// <param name="category">Category name</param>
    /// <returns>List of expenses for the category</returns>
    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category)
    {
        return await DbContext.Expenses
            .Where(e => e.Category == category)
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
    }

    /// <summary>
    /// Gets total expenses for a specific period
    /// </summary>
    /// <param name="startDate">Start date (inclusive)</param>
    /// <param name="endDate">End date (inclusive)</param>
    /// <returns>Total amount of expenses for the period</returns>
    public async Task<decimal> GetTotalExpensesForPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await DbContext.Expenses
            .Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate)
            .SumAsync(e => e.Amount);
    }
}
