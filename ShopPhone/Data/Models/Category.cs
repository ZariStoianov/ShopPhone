namespace ShopPhone.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; }

        public IEnumerable<Phone> Phones { get; set; } = new List<Phone>();
    }
}
