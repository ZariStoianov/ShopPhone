namespace ShopPhone.Models.Api.Phones
{
    using ShopPhone.Models.Phones;

    public class AllPhonesApiRequestModel
    {
        public string Brand { get; set; }

        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int PhonePerPage { get; set; } = 10;

        public AllPhonesSorting Sorting { get; set; }
    }
}
