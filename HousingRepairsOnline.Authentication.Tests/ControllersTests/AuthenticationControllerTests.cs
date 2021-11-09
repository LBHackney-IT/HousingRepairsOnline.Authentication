using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HousingRepairsOnline.Authentication.Controllers;
using HousingRepairsOnline.Authentication.Helpers;
using Moq;
using Xunit;

namespace HousingRepairsOnline.Authentication.Tests.ControllersTests
{
    public class AuthenticationControllerTests
    {
        private AuthenticationController systemUnderTest;
        private Mock<IIdentifierValidator> identifierValidatorMock;
        private Mock<IJwtHelper> jwtHelperMock;

        public AuthenticationControllerTests()
        {
            this.identifierValidatorMock = new Mock<IIdentifierValidator>();
            this.jwtHelperMock = new Mock<IJwtHelper>();
            this.systemUnderTest = new AuthenticationController(this.identifierValidatorMock.Object, this.jwtHelperMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("   ")]
        public async Task GivenAnInvalidIdentifier_WhenAuthenticateIsCalled_ThenUnauthorisedResponseIsGiven(string identifier)
        {
            // Arrange
            this.identifierValidatorMock.Setup(x => x.Validate(It.IsAny<string>())).Returns(false);

            // Act
            var result = await this.systemUnderTest.Authenticate(identifier);

            // Assert
            result.GetStatusCode().Should().Be((int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenAValidIdentifierThatIsNotAuthorised_WhenAuthenticateIsCalled_ThenUnauthorisedResponseIsGiven()
        {
            // Arrange
            const string identifier = "M3";
            this.identifierValidatorMock.Setup(x => x.Validate(identifier)).Returns(false);

            // Act
            var result = await this.systemUnderTest.Authenticate(identifier);

            // Assert
            result.GetStatusCode().Should().Be((int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GivenAValidIdentifierThatIsAuthorised_WhenAuthenticateIsCalled_ThenOkResponseIsGiven()
        {
            // Arrange
            const string identifier = "M3";
            this.identifierValidatorMock.Setup(x => x.Validate(identifier)).Returns(true);

            // Act
            var result = await this.systemUnderTest.Authenticate(identifier);

            // Assert
            result.GetStatusCode().Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenAValidIdentifierThatIsAuthorised_WhenAuthenticateIsCalled_ThenResponseContainsANonNullAndNonEmptyString()
        {
            // Arrange
            const string identifier = "M3";
            this.identifierValidatorMock.Setup(x => x.Validate(identifier)).Returns(true);
            this.jwtHelperMock.Setup(x => x.Generate()).Returns("a token");

            // Act
            var result = await this.systemUnderTest.Authenticate(identifier);

            // Assert
            result.GetResultData<string>().Should().NotBeNull().And.NotBeEmpty();
        }
    }


}
