using System;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace GameStore.WEB.UnitTests.Routs
{
    public static class RouteTestHelpers
    {
        public static void AssertIsRouteOf(RouteCollection routes, string url, object expectations)
        {
            // Arrange
            // Create the mock object for the HttpContextBase object
            var httpContextMock = new Mock<HttpContextBase>();
            // Simulate the request
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns(url);
            // Act
            // Get the route information based on the mock object
            var routeData = routes.GetRouteData(httpContextMock.Object);
            // Assert
            // Make sure a route will process the resource
            Assert.NotNull(routeData);
            foreach (var kvp in new RouteValueDictionary(expectations))
                Assert.True(string.Equals(kvp.Value.ToString(),
                        routeData.Values[kvp.Key].ToString(),
                        StringComparison.OrdinalIgnoreCase)
                    , string.Format("Expected '{0}', not '{1}' for '{2}'.",
                        kvp.Value, routeData.Values[kvp.Key], kvp.Key));
        }

        public static void AssertIsNotRouteOf(RouteCollection routes, string url, object expectations)
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            var routeData = routes.GetRouteData(httpContextMock.Object);
            if (routeData != null)
                foreach (var kvp in new RouteValueDictionary(expectations))
                    Assert.IsFalse(string.Equals(kvp.Value.ToString(),
                            routeData.Values[kvp.Key].ToString(),
                            StringComparison.OrdinalIgnoreCase)
                        , string.Format("Expected '{0}', not '{1}' for '{2}'.",
                            kvp.Value, routeData.Values[kvp.Key], kvp.Key));
        }
    }
}