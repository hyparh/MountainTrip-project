using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Data.Models
{
    using static DataConstants.Guide;

    public class Guide
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Trip> Trips { get; init; } = new HashSet<Trip>();
    }
}
