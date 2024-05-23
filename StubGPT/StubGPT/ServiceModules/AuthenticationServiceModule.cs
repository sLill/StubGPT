namespace StubGPT;
public class AuthenticationServiceModule : IServiceModule<IConfiguration>
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var authenticationConfiguration = configuration.GetSection("ApplicationConfiguration").Get<ApplicationConfiguration>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = authenticationConfiguration.SessionToken_Issuer,
                ValidAudience = authenticationConfiguration.SessionToken_Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.SessionToken_IssuerSigningKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
    #endregion Methods..
}
