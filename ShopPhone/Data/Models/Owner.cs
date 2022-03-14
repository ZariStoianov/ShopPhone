namespace ShopPhone.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Owner;
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(OwnerNameMaxLength, MinimumLength = OwnerNameMinLength)]
        public string FullName { get; set; }
    }
}
