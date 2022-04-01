namespace ShopPhone.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.User;
    public class User : IdentityUser
    {
        [StringLength(UserLastNameMaxLength, MinimumLength = UserLastNameMinLength)]
        public string FullName { get; set; }
    }
}
