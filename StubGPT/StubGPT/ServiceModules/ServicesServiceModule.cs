namespace StubGPT;
public class ServicesServiceModule : IServiceModule
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatApiService, ChatGPTApiService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        return services;
    }
    #endregion Methods..
}
