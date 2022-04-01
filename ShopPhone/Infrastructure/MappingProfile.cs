namespace ShopPhone.Infrastructure
{
    using AutoMapper;
    using ShopPhone.Models;
    using ShopPhone.Services.Phones.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<PhoneDetailsServiceModel, PhoneFormModel>();
        }
    }
}
