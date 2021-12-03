namespace HousingRepairsOnline.Authentication.Tests.HelpersTests
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Helpers;
    using Moq;
    using Xunit;

    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void GivenHttpClientWithoutBaseAddress_WhenSettingUpJwtAuthentication_InvalidOperationExceptionIsThrown()
        {
            // Arrange
            var httpRequestMessageMock = new Mock<HttpRequestMessage>();

            // Act
            Action act = () => httpRequestMessageMock.Object.SetupJwtAuthentication(new HttpClient(), "");

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
