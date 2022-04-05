namespace ShopPhone.Models.Owners
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Owner;

    public class BecomeOwnerFormModel
    {
        [Required]
        [StringLength(OwnerNameMaxLength, MinimumLength = OwnerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(OwnerPhoneMaxValue, MinimumLength = OwnerPhoneMinValue)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
