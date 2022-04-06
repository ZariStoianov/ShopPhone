namespace ShopPhone.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using ShopPhone.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapper()
            => MyRouting
            .Configuration()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldReturnView()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());

    }
}
