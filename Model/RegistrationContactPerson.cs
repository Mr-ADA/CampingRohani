using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{
    public class RegistrationContactPerson
    {
        [Key]
        public int RegistrationContactPersonId { get; set; }

        // Foreign key to Registration
        [ForeignKey(nameof(Registration))]
        public string RegistrationId { get; set; } = string.Empty;
        public Registration Registration { get; set; } = null!;

        // Foreign key to ContactPerson
        [ForeignKey(nameof(ContactPerson))]
        public int ContactPersonId { get; set; }
        public ContactPerson ContactPerson { get; set; } = null!;

        // Assignment metadata
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
        public bool IsPrimary { get; set; } = false; // Mark if this is the primary contact
        public string? Notes { get; set; } // Notes on assignment reason
    }
}
