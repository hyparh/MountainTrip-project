using MountainTrip.Data;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Guides
{
    using static DataConstants.Guide;

    public class BecomeGuideFormModel
    {
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        // may add regex validation here
        public string PhoneNumber { get; set; }
    }
}
