namespace HousingRepairsOnline.Authentication.Tests.HelpersTests
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Helpers;
    using Xunit;

    public class HttpClientExtensionsTests
    {
        [Fact]
        public void GivenHttpClientWithoutBaseAddress_WhenRetrievingAuthenticationToken_InvalidOperationExceptionIsThrown()
        {
            // Arrange

            // Act
            Action act = () => new HttpClient().RetrieveAuthenticationToken("").Wait();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
