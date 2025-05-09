using System;

namespace Khouissi_Caisse.Models;

/// <summary>
/// Represents a monthly subscription payment made by a member
/// </summary>
public class Subscription
{
    /// <summary>
    /// Unique identifier for the subscription
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Member ID who made the payment
    /// </summary>
    public int MemberId { get; set; }

    /// <summary>
    /// Navigation property to the member
    /// </summary>
    public Member? Member { get; set; }

    /// <summary>
    /// The month and year this subscription is for (stored as first day of the month)
    /// </summary>
    public DateTime PeriodDate { get; set; }

    /// <summary>
    /// Actual amount paid
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Default amount at the time of payment (reference for historical tracking)
    /// </summary>
    public decimal DefaultAmount { get; set; }

    /// <summary>
    /// Date the payment was made/recorded
    /// </summary>
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Notes about the payment
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if this is an advance payment for a future period
    /// </summary>
    public bool IsAdvancePayment { get; set; }

    /// <summary>
    /// Number of months covered by this payment (for advance payments)
    /// </summary>
    public int MonthsCovered { get; set; } = 1;

    /// <summary>
    /// User who recorded the subscription payment
    /// </summary>
    public int RecordedByUserId { get; set; }

    /// <summary>
    /// Navigation property to the user who recorded the payment
    /// </summary>
    public User? RecordedByUser { get; set; }
}
