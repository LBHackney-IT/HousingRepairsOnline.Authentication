namespace HousingRepairsOnline.Authentication.Helpers
{
    /// <summary>Abstraction of a JWT helper.</summary>
    public interface IJwtHelper
    {
        /// <summary>Generates a JWT.</summary>
        /// <returns>A JWT.</returns>
        string Generate();
    }
}
