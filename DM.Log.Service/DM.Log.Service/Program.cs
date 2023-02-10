

namespace DM.Log.Service
{
    using DM.Log.Biz;
    using DM.Log.Biz.Interface;
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


            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            Configure(app, app.Lifetime, app.Configuration);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddDbContext<LogDBContext>();

            //services.AddScoped<RequestInfo>();
            //services.AddScoped<RequestInfo>();
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            //// add grpc client

            services.AddScoped<IDotaRunService, DotaRunService>();
            services.AddScoped<ILogInterfaceService, LogInterfaceService>();




            ////如果需要让API/Grpc service访问Grpc service附上自身的token, 这一句必须加上(1/2)
            //services.AddHttpContextAccessor();

            #region 配置跨域 Cors
            //JwtCertConfig.Config.Path = configuration["JwtCertConfig:Path"]?.Replace('\\', Path.DirectorySeparatorChar);
            //JwtCertConfig.Config.Pwd = configuration["JwtCertConfig:Pwd"];
            
            var hostList = configuration.GetSection("Cors").GetChildren().Select(w => w.Value).ToArray();
            services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("cors", setupAction =>
                {
                    //setupAction.AllowAnyOrigin();
                    setupAction.AllowAnyHeader();
                    setupAction.AllowAnyMethod();
                    setupAction.AllowCredentials().WithOrigins(hostList);
                });
            });
            #endregion

        }

        private static void Configure(
            WebApplication app,
            IHostApplicationLifetime lifetime,
            IConfiguration configuration
            //IHttpContextAccessor hca
            )
        {
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 配置跨域
            app.UseCors("cors");





            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();




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