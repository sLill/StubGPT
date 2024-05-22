namespace StubGPT;
public class HttpServiceModule : IServiceModule
{
    #region Methods..
    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddHttpClient();
        services.AddRateLimiter(options => options.AddFixedWindowLimiter(policyName: "fixed", options =>
        {
            options.PermitLimit = 10;
            options.Window = TimeSpan.FromSeconds(10);
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            options.QueueLimit = 2;
        }));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        return services;
    }
    #endregion Methods..
}
