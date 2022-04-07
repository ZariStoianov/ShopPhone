namespace ShopPhone.Services.Phones.Models
{
    public interface IPhoneModel
    {
        string Brand { get; }

        string Model { get; }

        int Year { get; }
    }
}
