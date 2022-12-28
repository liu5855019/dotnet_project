

namespace DM.Log.Service
{
    using DM.Log.Common;
    using DM.Log.Dal;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder!
                .Host
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddNLog(new NLogProviderOptions { IncludeActivityIdsWithBeginScope = true });
                });



            //builder.Services.setdbcontext

            ConfigureServices(builder.Services, builder.Configuration);




            var app = builder.Build();

            Configure(app, app.Lifetime, app.Configuration);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            services.AddDbContext<LogDBContext>();

            services.AddScoped<RequestInfo>();

            //// add grpc client
            //var optionBuilder = new GrpcClientOptionBuilder(configuration);
            //services.AddGrpcGroupClient(optionBuilder.CurrentOption);
            //services.AddHttpClientProxyService(configuration.GetSection("HttpClientOption"), null);
            //// health check
            //services.AddTransient<IHttp1GrpcHealthService, Http1GrpcHealthService>();
            //services.AddTransient<IPublisher, PublisherImpl>();


            //services.AddScoped<RequestInfo>();
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<IScenarioService, ScenarioService>();
            //services.AddScoped<IScenarioRecordService, ScenarioRecordService>();
            //services.AddScoped<IncidentService>();
            //services.AddScoped<RmsService>();
            //services.AddScoped<PvnsService>();
            //services.AddScoped<ResStatusService>();
            //services.AddScoped<GisService>();
            //services.AddScoped<DumpService>();
            //services.AddScoped<IFileService, FileService>();

            //services.AddSingleton<IRunService, RunService>();
            //services.AddSingleton<IDteMessageBusPublish, DteMessageBusPublish>();
            //services.AddSingleton<DteMessageBusSubscribe>();
            //services.AddSingleton<IVehicleTripDetailService, VehicleTripDetailService>();


            ////如果需要让API/Grpc service访问Grpc service附上自身的token, 这一句必须加上(1/2)
            //services.AddHttpContextAccessor();

            //services.AddControllers();
            //#region Swagger
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1.0",
            //        Title = $"Dte Service Api ——{RuntimeInformation.FrameworkDescription}"
            //    });
            //});
            //#endregion

            //// Cors
            //services.AddCors(setupAction =>
            //{
            //    setupAction.AddPolicy("all", setupAction =>
            //    {
            //        setupAction.AllowAnyOrigin();
            //        setupAction.AllowAnyHeader();
            //        setupAction.AllowAnyMethod();
            //    });
            //});

        }

        private static void Configure(
            WebApplication app,
            IHostApplicationLifetime lifetime,
            IConfiguration configuration
            //IHttpContextAccessor hca
            )
        {
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dte Service Api v1.0");
            //});
            //app.UseCors("all");
            //app.MapControllers();

            //// Configure the HTTP request pipeline.
            //app.MapGrpcService<ScenarioGrpcSrv>();
            //app.MapGrpcService<ScenarioRecordGrpcSrv>();

            //app.MapGrpcReflectionService();

            //app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            ////如果需要让API/Grpc service访问Grpc service附上自身的token, 这一句必须加上(2/2)
            //GrpcClientInterceptor.SetHttpContextAccessor(app.Services.GetRequiredService<IHttpContextAccessor>());

            //// token 验证
            //JwtCertConfig.Config.Path = configuration["JwtCertConfig:Path"]?.Replace('\\', Path.DirectorySeparatorChar);
            //JwtCertConfig.Config.Pwd = configuration["JwtCertConfig:Pwd"];
            //app.UseTokenResovler(
            //    new X509Certificate2(
            //        JwtCertConfig.Config.Path,
            //        JwtCertConfig.Config.Pwd
            //    )
            //);



            lifetime.ApplicationStarted.Register(() =>
            {
                System.Console.WriteLine("ApplicationStarted");
                var scope = app.Services.CreateScope();
                var dBContext = scope.ServiceProvider.GetRequiredService<LogDBContext>();
                System.Console.WriteLine(dBContext);

                var a = dBContext.LogInterface.FirstOrDefault();
                System.Console.WriteLine(a);
            });






            //lifetime.ApplicationStopped.Register(() =>
            //{
            //    //app.Services.GetRequiredService<IComService>().CloseTerminal();
            //    logger.Info("App stopped");
            //});
        }

    }
}