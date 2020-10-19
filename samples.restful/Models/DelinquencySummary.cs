using System;
namespace samples.restful.Models
{
    public class DelinquencySummary
    {
        public DelinquencySummary()
        {
            
        }
        public string AccountId { get; set; }
        public string OwnerName { get; set; }
        public string UnitAddress { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal Total30Days { get; set; }
        public decimal Total60Days { get; set; }
        public decimal Total90Days { get; set; }
        public decimal TotalOver90Days { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public decimal LastPaymentAmount { get; set; }
        public string MailAddress { get; set; }
        public string AccountStatus { get; set; }
        public string DelinquencyStatus { get; set; }

    }
}
