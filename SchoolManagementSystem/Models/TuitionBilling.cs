namespace SchoolManagementSystem.Models
{
    public class TuitionBilling
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillingDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public ICollection<TuitionPayment> TuitionPayments { get; set; }
    }
}
