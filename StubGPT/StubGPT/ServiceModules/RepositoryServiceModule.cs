namespace StubGPT;
public class RepositoryServiceModule : IServiceModule
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserConfigurationRepository, UserConfigurationRepository>();
        builder.Services.AddScoped<IUserPromptRepository, UserPromptRepository>();
        return services;
    }
    #endregion Methods..
}
