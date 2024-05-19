namespace StubGPT.Common;
public static class ServiceCollectionExtensions
{
    #region Methods..
    public static IServiceCollection AddServiceModule<TServiceModule>(this IServiceCollection services) where TServiceModule : IServiceModule, new()
    {
        TServiceModule module = new();

        module.AddServices(services);
        return services;
    }

    public static IServiceCollection AddServiceModule<TServiceModule, TServiceData>(this IServiceCollection services, [Optional] TServiceData data) where TServiceModule : IServiceModule<TServiceData>, new()
    {
        TServiceModule module = new();

        module.AddServices(services, data);
        return services;
    }
    #endregion Methods..
}
