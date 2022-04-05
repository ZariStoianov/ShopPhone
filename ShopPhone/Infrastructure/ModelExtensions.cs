namespace ShopPhone.Infrastructure
{
    using ShopPhone.Services.Phones.Models;

    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this IPhoneModel phone)
        {
            return phone.Brand + "-" + phone.Model + "-" + phone.Year;
        }
    }
}
