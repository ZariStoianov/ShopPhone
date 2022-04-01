using ShopPhone.Areas.Admin;

namespace ShopPhone.Infrastructure
{
    using System.Security.Claims;

    using static AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdministratorRoleName);
        }
    }
}
