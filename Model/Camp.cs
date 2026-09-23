using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{
    public class Camp
    {
        [Key]
        public int CampId { get; set; }
        //CAMPING ROHANI SD 2026, CAMPING ROHANI SMP-SMA 2026, CAMPING ROHANI MAHASISWA 2026
        public string CampName { get; set; } = "";
        public decimal Price { get; set; } = 0;
        public string CampLocation { get; set; } = "";
        public string CampTheme { get; set; } = "";
        public string ParticipantCategory { get; set; } = "";
        public bool IsRegistrationOpen { get; set; } = true;
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
        public ICollection<Region> Regions { get; set; } = new List<Region>();
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
} 
