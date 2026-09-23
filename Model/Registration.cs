using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{
    public class Registration
    {
        [Key]
        public required string RegistrationId { get; set; } = "";
        // Many-to-one: Many registrations point to one participant
        public required string ParticipantId { get; set; } = "";
        [ForeignKey(nameof(ParticipantId))]
        public required Participant Participant { get; set; } = null!;
        public string? PaymentId { get; set; }
        public Payment? Payment { get; set; }
        // Many-to-one: Many registrations point to one camp
        public int? CampId { get; set; } = 0;
        [ForeignKey(nameof(CampId))]
        public Camp? Camp { get; set; }
        // Many-to-many: A registration can have multiple contact persons
        public ICollection<RegistrationContactPerson> ContactPersonAssignments { get; set; } = new List<RegistrationContactPerson>();
        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
