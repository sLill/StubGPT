namespace StubGPT;
public class Program
{
    #region Methods..
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Settings
        builder.Configuration.AddJsonFile("appsettings.json");
        //builder.Services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);

        // Services
        builder.Services.AddScoped<IOpenAIService, OpenAIService>();
        builder.Services.AddAuthorization();
        builder.Services.AddHttpClient();

        // Web Host
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        builder.WebHost.ConfigureKestrel(ConfigureKestrelHost);

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
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();

        //app.UseCors("AllowAnyOrigin");
        //app.UseAuthorization();

        //app.UseEndpoints(endpoints =>
        //{
        //    SystemEndpoints.Register(endpoints);
        //    PlayerEndpoints.Register(endpoints);
        //});
    }

    private static void ConfigureKestrelHost(WebHostBuilderContext hostContext, KestrelServerOptions options)
    {
        options.ListenAnyIP(5080);
        options.ListenAnyIP(5081, options => options.UseHttps());
    } 
    #endregion Methods..
}