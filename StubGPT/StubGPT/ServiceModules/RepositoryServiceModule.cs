namespace StubGPT;
public class RepositoryServiceModule : IServiceModule
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserSessionRepository, UserSessionRepository>();
        services.AddScoped<IUserConfigurationRepository, UserConfigurationRepository>();
        services.AddScoped<IUserPromptRepository, UserPromptRepository>();
        return services;
    }
    #endregion Methods..
}
