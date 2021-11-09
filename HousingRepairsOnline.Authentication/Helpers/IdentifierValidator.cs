using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;

namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>A simple identifier validator that validates against a single known value.</summary>
    public class IdentifierValidator : IIdentifierValidator
    {
        private readonly string identifier;

        /// <summary>The constructor.</summary>
        /// <param name="identifier">The (non-null, non-empty) identifier to be used for validation.</param>
        public IdentifierValidator([NotNull] string identifier)
        {
            Guard.Against.NullOrWhiteSpace(identifier, nameof(identifier));

            this.identifier = identifier;
        }


        /// <inheritdoc />
        public bool Validate(string identifier)
        {
            return identifier == this.identifier;
        }
    }
}
