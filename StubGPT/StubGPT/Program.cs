namespace StubGPT;
public class Program
{
    #region Methods..
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Settings
        builder.Configuration.Sources.Clear();
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
        builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("AuthenticationConfiguration"));
        builder.Services.AddTransient<IConfigureOptions<UserConfiguration>, UserConfigurationOptionsProvider>();

        // Logging
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddProvider(new SqlLoggerProvider((category, level) => level >= LogLevel.Error, GetMainConnectionString(builder)));
        });

        // Services
        builder.Services.AddServiceModule<ConfigurationServiceModule, IConfiguration>(builder.Configuration);
        builder.Services.AddServiceModule<AuthenticationServiceModule, IConfiguration>(builder.Configuration);
        builder.Services.AddServiceModule<ServicesServiceModule>();
        builder.Services.AddServiceModule<HttpServiceModule>();
        builder.Services.AddServiceModule<SwaggerServiceModule>();
        builder.Services.AddServiceModule<RepositoryServiceModule>();

        // Web Host
        builder.WebHost.ConfigureKestrel(ConfigureKestrelHost);

        // Data
        builder.Services.AddDbContext<MainDbContext>(options =>
        {
            options.UseSqlServer(GetMainConnectionString(builder));

            if (builder.Environment.IsDevelopment())
                options.EnableSensitiveDataLogging();
        });

        var app = builder.Build();

        ConfigureApplication(app);

        app.Run();
    }

    private static void ConfigureApplication(WebApplication app)
    {
        app.UseExceptionHandler("/error");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        app.UseForwardedHeaders();
        //app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors("AllowAnyOrigin");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            MessageEndpoints.Register(endpoints);
            UserEndpoints.Register(endpoints);
        });
    }

    private static void ConfigureKestrelHost(WebHostBuilderContext hostContext, KestrelServerOptions options)
    {
        options.ListenAnyIP(5110);
        //options.ListenAnyIP(5111, options => options.UseHttps());
    }

    private static string GetMainConnectionString(WebApplicationBuilder builder)
    {
        string connectionString = string.Empty;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            connectionString = builder.Configuration.GetConnectionString("ConnectionString_Main")!;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            connectionString = builder.Configuration["ConnectionString_Main"]!;

        return connectionString;
    }
    #endregion Methods..
}