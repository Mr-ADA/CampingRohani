using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{
    public class ContactPerson
    {
        [Key]
        public int ContactPersonId { get; set; }

        // Many-to-one: Many contact persons belong to one region
        public int RegionId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; } = null!;

        // Many-to-many: A contact person handles multiple registrations
        public ICollection<RegistrationContactPerson> AssignedRegistrations { get; set; } = new List<RegistrationContactPerson>();

        // One-to-many: One contact person has many payments
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public string AccountName { get; set; } = "";
        public string BankName { get; set; } = "";

        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
