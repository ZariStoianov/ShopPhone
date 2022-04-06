namespace ShopPhone.Test.Owners
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using ShopPhone.Controllers;
    using ShopPhone.Models.Owners;

    public class OwnersControllerTest
    {
        [Fact]
        public void BecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Owners/Create")
            .To<OwnersController>(c => c.Create());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Owners/Create")
            .WithMethod(HttpMethod.Post))
            .To<OwnersController>(c => c.Create(With.Any<BecomeOwnerFormModel>()));
    }

}
