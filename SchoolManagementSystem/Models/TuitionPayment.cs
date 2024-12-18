namespace SchoolManagementSystem.Models
{
    public class TuitionPayment
    {
        public int Id { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TuitionBillingId { get; set; }
        public TuitionBilling TuitionBilling { get; set; } // Navigation property
    }
}
