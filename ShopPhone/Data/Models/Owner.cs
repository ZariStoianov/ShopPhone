namespace ShopPhone.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Owner;
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(OwnerNameMaxLength, MinimumLength = OwnerNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(OwnerPhoneMaxValue, MinimumLength = OwnerPhoneMinValue)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Phone> Phones { get; set; } = new List<Phone>();
    }
}
