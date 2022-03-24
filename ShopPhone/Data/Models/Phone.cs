namespace ShopPhone.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Phone;
    public class Phone
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PhoneBrandMaxLength, MinimumLength = PhoneBrandMinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(PhoneModelMaxLength, MinimumLength = PhoneModelMinLength)]
        public string Model { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(PhoneYearMaxValue, MinimumLength = PhoneYearMinValue)]
        public int Year { get; set; }

        [Required]
        [StringLength(PhoneDescriptionMaxLength, MinimumLength = PhoneDescriptionMinLength)]
        public string Description { get; set; }

        public int CategotyId { get; set; }

        public Category Category { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
