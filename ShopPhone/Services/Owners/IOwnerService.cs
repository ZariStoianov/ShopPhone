namespace ShopPhone.Services.Owners
{
    public interface IOwnerService
    {
        public bool IsOwner(string userId);
        public int GetIdByUser(string userId);
    }
}
