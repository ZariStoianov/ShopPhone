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
        [StringLength(PhoneNumberMaxValue, MinimumLength = PhoneNumberMinValue)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(PhoneYearMaxValue, MinimumLength = PhoneYearMinValue)]
        public int Year { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal PriceForPhone { get; set; }

        [Required]
        [StringLength(PhoneDescriptionMaxLength, MinimumLength = PhoneDescriptionMinLength)]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
