namespace ShopPhone.Services.Phones.Models
{
    public class PhoneServiceModel : IPhoneModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public decimal PriceForPhone { get; set; }

        public int Year { get; set; }

        public bool IsPublic { get; set; }

        public string CategoryName { get; set; }
    }
}
