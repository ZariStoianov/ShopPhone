namespace ShopPhone.Models.Phones
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPhonesQueryModel
    {

        public const int PhonePerPage = 3;

        public string Brand { get; set; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPhones { get; set; }

        public AllPhonesSorting Sorting { get; set; }

        public IEnumerable<PhoneListingViewModel> Phones { get; set; }
    }
}
