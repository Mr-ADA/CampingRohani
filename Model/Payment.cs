using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; } = "";

        // ✓ NULLABLE - Allows SET NULL on delete
        public string? RegistrationId { get; set; }
        [ForeignKey(nameof(RegistrationId))]
        public Registration? Registration { get; set; }

        public int? ContactPersonId { get; set; }
        [ForeignKey(nameof(ContactPersonId))]
        public ContactPerson? ContactPerson { get; set; }

        public int? CampId { get; set; }
        [ForeignKey(nameof(CampId))]
        public Camp? Camp { get; set; }

        public decimal Amount { get; set; } = 0;
        public PaymentType PaymentType { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? DirectoryProofImage { get; set; }
        public DateTime PaymentTime { get; set; } = DateTime.UtcNow;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
