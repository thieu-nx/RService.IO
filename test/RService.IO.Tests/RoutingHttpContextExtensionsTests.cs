﻿using System;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Moq;
using RService.IO.Router;
using Xunit;
using RoutingHttpContextExtensions = RService.IO.Abstractions.RoutingHttpContextExtensions;

namespace RService.IO.Tests
{
    public class RoutingHttpContextExtensionsTests
    {
        [Fact]
        public void GetRouteHandler__ThrowsArguementNullExceptionOnNullContext()
        {
            Action comparison = () => { RoutingHttpContextExtensions.GetRouteHandler(null); };

            comparison.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetRouteHandler__GetsHandlerFromRServiceRoutingFeature()
        {
            var context = new Mock<HttpContext>().SetupAllProperties();
            var features = new Mock<IFeatureCollection>().SetupAllProperties();
            var routingFeature = new Mock<RoutingFeature>().SetupAllProperties();
            var routeHandler = new Mock<RequestDelegate>().SetupAllProperties();
            context.SetupGet(x => x.Features).Returns(features.Object);
            features.Setup(x => x[typeof(IRoutingFeature)]).Returns(routingFeature.Object);
            routingFeature.Object.RouteHandler = routeHandler.Object;

            var handle = RoutingHttpContextExtensions.GetRouteHandler(context.Object);

            handle.Should().NotBeNull();
        }

        [Fact]
        public void GetRouteHandler__ReturnsNullIfNotRServiceRoutingFeature()
        {
            var context = new Mock<HttpContext>().SetupAllProperties();
            var features = new Mock<IFeatureCollection>().SetupAllProperties();
            var routingFeature = new Mock<IRoutingFeature>().SetupAllProperties();
            context.SetupGet(x => x.Features).Returns(features.Object);
            features.Setup(x => x[typeof(IRoutingFeature)]).Returns(routingFeature.Object);

            var handle = RoutingHttpContextExtensions.GetRouteHandler(context.Object);

            handle.Should().BeNull();
        }
    }
}