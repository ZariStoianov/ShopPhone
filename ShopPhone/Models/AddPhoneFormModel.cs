using System.ComponentModel.DataAnnotations;

namespace ShopPhone.Models
{
    public class AddPhoneFormModel
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}
