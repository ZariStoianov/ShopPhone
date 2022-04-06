namespace ShopPhone.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using ShopPhone.Controllers;
    using ShopPhone.Data.Models;
    using ShopPhone.Models.Owners;
    using System.Linq;
    using Xunit;

    using static ShopPhone.WebConstants;

    public class OwnersControllerTest
    {
        [Fact]
        public void BecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<OwnersController>
                .Instance()
                 .Calling(c => c.Create())
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests())
                 .AndAlso()
                 .ShouldReturn()
                 .View();

        [Theory]
        [InlineData("Owner", "0885025332")]
        public void BecomeShouldBeAuthorizedUsersAndReturnView(string ownerName, string phoneNumber)
            => MyController<OwnersController>
            .Instance(instance => instance
            .WithUser(TestUser.Identifier))
            .Calling(c => c.Create(new BecomeOwnerFormModel
            {
                Name = ownerName,
                PhoneNumber = phoneNumber
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data.WithSet<Owner>(owner => 
                owner.Any(o =>
                o.FullName == ownerName &&
                o.PhoneNumber == phoneNumber &&
                o.UserId == TestUser.Identifier)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("All", "Phones");
    }
}
