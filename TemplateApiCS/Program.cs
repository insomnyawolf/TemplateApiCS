using DoctorateCS;
using Microsoft.Extensions.Configuration;
using TemplateApiCS.Database;
using TemplateApiCS.Middlewares;

namespace TemplateApiCS
{
    public static class Program
    {
        public static WebApplication WebApplication { get; set; }
        public static IServiceProvider IServiceProvider => WebApplication.Services;
        public static IConfiguration IConfiguration => WebApplication.Configuration;
        public static AppSettings AppSettings => IConfiguration.GetRequiredSection(nameof(AppSettings)).Get<AppSettings>();
        public static ILoggerFactory ILoggerFactory => WebApplication.Services.GetService<ILoggerFactory>();
        public static void Main(string[] args)
        {
            // https://docs.microsoft.com/es-es/dotnet/core/extensions/configuration-providers
            IConfigurationBuilder IConfigurationBuilder = new ConfigurationBuilder();
            IConfigurationBuilder = IConfigurationBuilder.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
            var iConfiguration = IConfigurationBuilder.Build();

            var iLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConfiguration(iConfiguration)
                    //.SetMinimumLevel(AppSettings.LogLevel)
                    //.AddFilter("Microsoft", LogLevel.Trace)
                    //.AddFilter("System", LogLevel.Trace)
                    //.AddFilter("LoggingConsoleApp.Program", LogLevel.Trace)
                    .AddConsole()
                    //.AddEventLog()
                    ;
            });

            var builder = WebApplication.CreateBuilder(args);

            // Setup Configuration
            var config = builder.Configuration;

            config.AddConfiguration(iConfiguration);

            // Add services to the container.
            var IServiceCollection = builder.Services;

            IServiceCollection.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            IServiceCollection.AddEndpointsApiExplorer();
            IServiceCollection.AddSwaggerGen();

            IServiceCollection.AddHttpClient();
            IServiceCollection.AddSingleton(iLoggerFactory);
            IServiceCollection.AddScoped<AppSettings>((_) => AppSettings);

            //            IServiceCollection.AddSqlite<DatabaseContext>(iConfiguration.GetConnectionString("SampleContext"));

            //            IServiceCollection.AddDbContext<DatabaseContext>((builderOptions) =>
            //            {
            //#if DEBUG
            //                builderOptions.EnableDetailedErrors();
            //                builderOptions.EnableSensitiveDataLogging();

            //#endif
            //            });

            // Http file browser
            // Maybe not needed
            //IServiceCollection.AddDirectoryBrowser();

            WebApplication = builder.Build();

            // Configure the HTTP request pipeline.
            if (WebApplication.Environment.IsDevelopment())
            {
                WebApplication.UseDeveloperExceptionPage();
                WebApplication.UseSwagger();
                WebApplication.UseSwaggerUI();
            }

            WebApplication.UseMiddleware<CustomExceptionHandlerMiddleware>();

            WebApplication.UseRouting();

            //WebApplication.UseAuthorization();

            WebApplication.MapControllers();

            // Can be configured if needed
            WebApplication.UseStaticFiles();
            // Can be configured if needed
            WebApplication.UseDirectoryBrowser();
            // Redirect to index automatically
            //WebApplication.UseDefaultFiles();

            WebApplication.Run();
        }
    }
}