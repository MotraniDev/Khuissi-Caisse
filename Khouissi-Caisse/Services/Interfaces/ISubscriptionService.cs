using System;

namespace Khouissi_Caisse.Services.Interfaces
{
    public class SubscriptionStatus
    {
        public string Status { get; set; } = string.Empty; // Initialize to empty string
        public bool IsActive { get; set; }
    }

    public interface ISubscriptionService
    {
        /// <summary>
        /// Gets the date of the last payment for a member
        /// </summary>
        DateTime? GetLastPaymentDate(int memberId);

        /// <summary>
        /// Gets the subscription status for a member
        /// </summary>
        SubscriptionStatus GetSubscriptionStatus(int memberId);

        /// <summary>
        /// Gets the advance payment amount for a member
        /// </summary>
        decimal GetAdvancePaymentAmount(int memberId);

        /// <summary>
        /// Records a payment for a member
        /// </summary>
        void RecordPayment(int memberId, decimal amount, DateTime paymentDate, int monthsCount = 1);
    }
}