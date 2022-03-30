namespace ShopPhone.Services.Phones
{
    public class PhoneDetailsServiceModel : PhoneServiceModel
    {
        public string Description { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
