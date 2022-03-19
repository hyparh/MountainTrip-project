using Xunit;
using MyTested.AspNetCore.Mvc;
using MountainTrip.Controllers;

namespace MountainTrip.Test.Controllers
{
    public class GuidesControllerTest
    {
        [Fact]
        public void BecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyRouting
            .Configuration()
            .ShouldMap("/Guides/Become")
            .To<GuidesController>(t => t.Create())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();
    }
}
