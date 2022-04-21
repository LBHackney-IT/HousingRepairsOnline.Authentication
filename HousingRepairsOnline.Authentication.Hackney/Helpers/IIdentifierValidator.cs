namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>Abstraction of an identifier validator.</summary>
    public interface IIdentifierValidator
    {
        /// <summary>Validates the provided <paramref name="identifier"/>.</summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>True, if identifier is valid; otherwise false.</returns>
        bool Validate(string identifier);
    }
}
