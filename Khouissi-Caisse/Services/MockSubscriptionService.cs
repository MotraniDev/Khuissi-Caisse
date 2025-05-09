using Khouissi_Caisse.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Khouissi_Caisse.Services
{
    public class MockSubscriptionService : ISubscriptionService
    {
        private class PaymentRecord
        {
            public int MemberId { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Amount { get; set; }
            public int MonthsCount { get; set; }
        }

        private readonly List<PaymentRecord> _payments;
        private readonly decimal _monthlySubscriptionAmount = 500.0m; // Default monthly amount

        public MockSubscriptionService()
        {
            // Initialize with sample data
            _payments = new List<PaymentRecord>
            {
                new PaymentRecord
                {
                    MemberId = 1,
                    PaymentDate = DateTime.Now.AddDays(-15),
                    Amount = 500.0m,
                    MonthsCount = 1
                },
                new PaymentRecord
                {
                    MemberId = 2,
                    PaymentDate = DateTime.Now.AddDays(-45),
                    Amount = 1500.0m,
                    MonthsCount = 3
                },
                new PaymentRecord
                {
                    MemberId = 3,
                    PaymentDate = DateTime.Now.AddMonths(-5),
                    Amount = 500.0m,
                    MonthsCount = 1
                }
            };
        }

        public DateTime? GetLastPaymentDate(int memberId)
        {
            return _payments
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => (DateTime?)p.PaymentDate)
                .FirstOrDefault();
        }

        public SubscriptionStatus GetSubscriptionStatus(int memberId)
        {
            var lastPayment = _payments
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefault();

            if (lastPayment == null)
            {
                return new SubscriptionStatus
                {
                    Status = "غير مشترك",
                    IsActive = false
                };
            }

            // Calculate paid-up until date
            var paidUpUntil = lastPayment.PaymentDate.AddMonths(lastPayment.MonthsCount);
            var currentDate = DateTime.Now;

            if (paidUpUntil >= currentDate)
            {
                return new SubscriptionStatus
                {
                    Status = $"مشترك حتى {paidUpUntil.ToString("dd/MM/yyyy")}",
                    IsActive = true
                };
            }
            else
            {
                var monthsOverdue = ((currentDate.Year - paidUpUntil.Year) * 12) + currentDate.Month - paidUpUntil.Month;
                return new SubscriptionStatus
                {
                    Status = $"متأخر {monthsOverdue} أشهر",
                    IsActive = false
                };
            }
        }

        public decimal GetAdvancePaymentAmount(int memberId)
        {
            var lastPayment = _payments
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefault();

            if (lastPayment == null)
            {
                return 0;
            }

            // Calculate paid-up until date
            var paidUpUntil = lastPayment.PaymentDate.AddMonths(lastPayment.MonthsCount);
            var currentDate = DateTime.Now;

            if (paidUpUntil > currentDate)
            {
                var monthsInAdvance = ((paidUpUntil.Year - currentDate.Year) * 12) + paidUpUntil.Month - currentDate.Month;
                return monthsInAdvance * _monthlySubscriptionAmount;
            }
            else
            {
                return 0;
            }
        }

        public void RecordPayment(int memberId, decimal amount, DateTime paymentDate, int monthsCount = 1)
        {
            _payments.Add(new PaymentRecord
            {
                MemberId = memberId,
                PaymentDate = paymentDate,
                Amount = amount,
                MonthsCount = monthsCount
            });
        }
    }
}