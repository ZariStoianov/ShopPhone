namespace ShopPhone.Models
{
    using ShopPhone.Services.Phones.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Phone;
    public class PhoneFormModel : IPhoneModel
    { 
        [Required]
        [StringLength(PhoneBrandMaxLength, MinimumLength = PhoneBrandMinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(PhoneModelMaxLength, MinimumLength = PhoneModelMinLength)]
        public string Model { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Range(PhoneYearMinValue, PhoneYearMaxValue)]
        public int Year { get; set; }

        [Required]
        public decimal PriceForPhone { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxValue, MinimumLength = PhoneNumberMinValue)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(PhoneDescriptionMaxLength, MinimumLength = PhoneDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<PhoneCategoryServiceModel> Categories { get; set; }
    }
}
