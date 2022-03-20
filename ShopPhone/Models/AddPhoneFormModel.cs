namespace ShopPhone.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Phone;
    public class AddPhoneFormModel
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
        [StringLength(PhoneDescriptionMaxLength, MinimumLength = PhoneDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<PhoneCategoryFormModel> Categories { get; set; }
    }
}
