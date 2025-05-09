using System;

namespace Khouissi_Caisse.Models;

/// <summary>
/// Represents an expense paid from the fund
/// </summary>
public class Expense
{
    /// <summary>
    /// Unique identifier for the expense
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date when the expense occurred
    /// </summary>
    public DateTime ExpenseDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Amount of the expense
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Beneficiary of the expense (person or entity receiving the money)
    /// </summary>
    public string Beneficiary { get; set; } = string.Empty;

    /// <summary>
    /// Purpose of the expense (reason for the payment)
    /// </summary>
    public string Purpose { get; set; } = string.Empty;

    /// <summary>
    /// Category of the expense (e.g., Utilities, Charitable, Administrative)
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Any additional notes about the expense
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Reference to any receipt or document related to the expense
    /// </summary>
    public byte[]? ReceiptImage { get; set; }

    /// <summary>
    /// ID of the user who recorded this expense
    /// </summary>
    public int RecordedByUserId { get; set; }

    /// <summary>
    /// Navigation property to the user who recorded the expense
    /// </summary>
    public User? RecordedByUser { get; set; }

    /// <summary>
    /// Payment method (Cash, Transfer, Check, etc.)
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Reference number for the payment (e.g., check number, transfer reference)
    /// </summary>
    public string PaymentReference { get; set; } = string.Empty;
}
