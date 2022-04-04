using MountainTrip.Data;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Services.Guides
{
    using static DataConstants.Guide;

    public class BecomeGuideFormModel
    {
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        // TODO may add regex validation here
        public string PhoneNumber { get; set; }
    }
}
