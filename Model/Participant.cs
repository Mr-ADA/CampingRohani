using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{

    public class Participant
    {
        [Key]
        public string ParticipantId { get; set; } = "";

        // One-to-one: One participant has one registration
        public Registration? Registration { get; set; }

        // Many-to-one: Many participants belong to one region
        public int RegionId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public Region? Region { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Parish { get; set; } = "";

        [MaxLength(2000)]
        public string AdditionalDescription { get; set; } = "";

        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
