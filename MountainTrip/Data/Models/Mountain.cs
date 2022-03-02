using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Data.Models
{
    using static DataConstants.Mountain;

    public class Mountain
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Trip> Trips { get; init; } = new HashSet<Trip>();
    }
}
