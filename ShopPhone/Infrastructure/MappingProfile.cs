namespace ShopPhone.Infrastructure
{
    using AutoMapper;
    using ShopPhone.Data.Models;
    using ShopPhone.Models;
    using ShopPhone.Services.Phones.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, PhoneCategoryServiceModel>();
            this.CreateMap<Phone, LatestPhoneServiceModel>();
            this.CreateMap<PhoneDetailsServiceModel, PhoneFormModel>();

            this.CreateMap<Phone, PhoneServiceModel>()
                .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));

            this.CreateMap<Phone, PhoneDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Owner.UserId))
                .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
