using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampingRohani.Model
{
    public class Region
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionName { get; set; } = "";
        // Many-to-one: Many regions belong to one camp
        public int CampId { get; set; }
        [ForeignKey(nameof(CampId))]
        public Camp Camp { get; set; } = null!;

        // One-to-many: One region has many contact persons
        public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();

        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
