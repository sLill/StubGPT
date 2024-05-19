namespace StubGPT;
public class AuthenticationServiceModule : IServiceModule<IConfiguration>
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
    {
        //var authenticationConfiguration = configuration.GetSection("AuthenticationConfiguration").Get<AuthenticationConfiguration>();

        //services.AddAuthentication(x =>
        //{
        //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(x =>
        //{
        //    x.TokenValidationParameters = new TokenValidationParameters()
        //    {
        //        ValidIssuer = authenticationConfiguration.Issuer,
        //        ValidAudience = authenticationConfiguration.Audience,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.Key)),
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ClockSkew = TimeSpan.Zero
        //    };
        //});
       
        return services;
    }
    #endregion Methods..
}
