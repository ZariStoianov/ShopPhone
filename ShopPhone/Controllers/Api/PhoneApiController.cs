namespace ShopPhone.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Models.Api.Phones;
    using ShopPhone.Services.Phones;
    using ShopPhone.Services.Phones.Models;

    [ApiController]
    [Route("api/phones")]
    public class PhoneApiController : ControllerBase
    {
        private readonly IPhoneService data;

        public PhoneApiController(IPhoneService data)
        {
            this.data = data;
        }


        [HttpGet]
        public PhoneQueryServiceModel All([FromQuery] AllPhonesApiRequestModel query)
        {
            return this.data.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.PhonePerPage);
        }
    }
}
