using System;
using FluentAssertions;
using HousingRepairsOnline.Authentication.Helpers;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Xunit;

namespace HousingRepairsOnline.Authentication.Tests.HelpersTests
{
    public class JwtHelperTests
    {
        private JwtHelper systemUnderTest = new("Secret", "Issuer", "Audience");

        [Fact]
        public void WhenATokenIsGenerated_ThenANonNullTokenIsGenerated()
        {
            // Arrange

            // Act
            var actual = this.systemUnderTest.Generate();

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void WhenATokenIsGenerated_ThenANonEmptyTokenIsGenerated()
        {
            // Arrange

            // Act
            var actual = this.systemUnderTest.Generate();

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void WhenATokenIsGenerated_ThenItIsUnexpired()
        {
            // Arrange
            //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MzYwOTc4ODEsImlzcyI6IkFuIElzc3VlciIsImF1ZCI6IlRoZSBBdWRpZW5jZSJ9.R9ck0EQAHuL4qbbnI1QUdV5nmYMnczkHr0-yhGmyNT8";

            // Act
            var actual = this.systemUnderTest.Generate();

            // Assert
            Action act = () => Decode(actual);

            act.Should().NotThrow<TokenExpiredException>();

            string Decode(string token)
            {
                return JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                    .WithSecret("Secret")
                    .MustVerifySignature()
                    .Decode(token);
            }
        }
    }
}
