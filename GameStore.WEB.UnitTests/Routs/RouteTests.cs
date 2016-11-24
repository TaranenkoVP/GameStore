using System.Web;
using System.Web.Routing;
using GameStore.WEB.MVC;
using Moq;
using NUnit.Framework;

namespace GameStore.WEB.UnitTests.Routs
{
    [TestFixture]
    public class RouteTests
    {
        private static RouteCollection _routes;

        [OneTimeSetUp]
        public void SetUp()
        {
            _routes = new RouteCollection();
            RouteConfig.RegisterRoutes(_routes);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            RouteTable.Routes.Clear();
        }

        [Test]
        [TestCase("Show")]
        [TestCase("ByGenre")]
        [TestCase("ByPlatformType")]
        public void Default_GamePageByKeyShouldMapTo_GameControllerWithActionByKey(string action)
        {
            RouteTestHelpers.AssertIsRouteOf(_routes, "~/Game/" + action + "/1",
                new {controller = "Game", action, key = "1"});
        }

        [Test]
        [TestCase("New")]
        [TestCase("Update")]
        [TestCase("Delete")]
        public void Default_GamePageShouldMapTo_GameControllerWithAction(string action)
        {
            RouteTestHelpers.AssertIsRouteOf(_routes, "~/Game/" + action,
                new {controller = "Game", action});
        }

        [Test]
        public void GameComments_GamePageShouldMapTo_GameControllerByKeyWithActionComments()
        {
            RouteTestHelpers.AssertIsRouteOf(_routes, "~/Game/1/comments",
                new {controller = "Game", action = "Comments", gamekey = "1"});
        }

        [Test]
        public void GameDownload_GamePageShouldMapTo_GameControllerByKeyWithActionComments()
        {
            RouteTestHelpers.AssertIsRouteOf(_routes, "~/Game/1/download",
                new {controller = "Game", action = "Download", gamekey = "1"});
        }

        [Test]
        public void ShouldIgnore_AXDResource_StopRoutingHandler()
        {
            // Create the mock object for the HttpContextBase object
            var mockContextBase = new Mock<HttpContextBase>();
            // Simulate the request for a resource of type {something}.axd
            mockContextBase.Setup(x => x.Request.AppRelativeCurrentExecutionFilePath).Returns("~/Resource.axd");
            // Get the route information based on the mock object
            var routeData = _routes.GetRouteData(mockContextBase.Object);
            // Make sure a route will process the resource
            Assert.IsNotNull(routeData);
            // Verify the type of route is of type StopRoutingHandler. This indicates
            // the request matches a URL pattern that won't use routing
            Assert.IsInstanceOf<StopRoutingHandler>(routeData.RouteHandler);
        }
    }
}