﻿namespace StubGPT;
public class ConfigurationServiceModule : IServiceModule<IConfiguration>
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
        services.Configure<AuthenticationConfiguration>(configuration.GetSection("AuthenticationConfiguration"));
        services.Configure<ChatGPTApiConfiguration>(configuration.GetSection("ChatGPTApiConfiguration"));

        return services;
    }
    #endregion Methods..
}
