namespace StubGPT;
public class Program
{
    #region Methods..
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Settings
        builder.Configuration.AddJsonFile("appsettings.json");

        // Services
        builder.Services.AddServiceModule<ConfigurationServiceModule, IConfiguration>(builder.Configuration);
        builder.Services.AddServiceModule<AuthenticationServiceModule, IConfiguration>(builder.Configuration);
        builder.Services.AddScoped<IChatApiService, ChatGPTApiService>();

        builder.Services.AddAuthorization();
        builder.Services.AddHttpClient();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenNewtonsoftSupport();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "StubGPT Api", Version = "v1" });
        });

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
        //app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors("AllowAnyOrigin");
        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            MessageEndpoints.Register(endpoints);
        });
    }

    private static void ConfigureKestrelHost(WebHostBuilderContext hostContext, KestrelServerOptions options)
    {
        options.ListenAnyIP(5110);
        //options.ListenAnyIP(5111, options => options.UseHttps());
    } 
    #endregion Methods..
}