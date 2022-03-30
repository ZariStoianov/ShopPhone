namespace ShopPhone.Services.Owners
{
    using ShopPhone.Data;
    using System.Linq;

    public class OwnerService : IOwnerService
    {

        private readonly ApplicationDbContext data;

        public OwnerService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool IsOwner(string userId)
        {
            return this.data
                .Owners
                .Any(o => o.UserId == userId);

        }

        public int GetIdByUser(string userId)
        {
            return this.data
                .Owners
                .Where(o => o.UserId == userId)
                .Select(o => o.Id)
                .FirstOrDefault();
        }
    }
}
