namespace PasswordManager;

/// <summary>
///     Provides the configuration keys for the application.
/// </summary>
public struct ConfigurationKeys
{
    /// <summary>
    ///     The key for the connection string in the configuration file.
    /// </summary>
    public const string ConnectionString = "DefaultConnection";

    /// <summary>
    ///     The keys for the JWT section in the configuration file.
    /// </summary>
    public struct Jwt
    {
        /// <summary>
        ///     The key for the JWT section in the configuration file.
        /// </summary>
        public const string SectionName = "Jwt";

        /// <summary>
        ///     The key for the issuer in the configuration file.
        /// </summary>
        public const string Issuer = "Jwt:Issuer";

        /// <summary>
        ///     The key for the audience in the configuration file.
        /// </summary>
        public const string Audience = "Jwt:Audience";

        /// <summary>
        ///     The key for the signing key in the configuration file.
        /// </summary>
        public const string SigningKey = "Jwt:SigningKey";
    }
}