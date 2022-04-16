namespace ShopPhone.Services.Phones.Models
{
    public class LatestPhoneServiceModel : IPhoneModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public string PhoneNumber { get; set; }

        public int Year { get; set; }
    }
}
