

namespace DM.Log.Service
{
    using DM.Log.Biz;
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Dal;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using NLog.Extensions.Logging;
    using System.IO;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.CompilerServices;

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

            #region regist basic
            services.AddDbContext<LogDBContext>();

            services.AddScoped<RequestInfo>();
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            ////如果需要让API/Grpc service访问Grpc service附上自身的token, 这一句必须加上(1/2)
            // 注册 IHttpContextAccessor
            services.AddHttpContextAccessor();
            #endregion


            #region regist service
            services.AddScoped<IDotaRunService, DotaRunService>();
            services.AddScoped<ILogInterfaceService, LogInterfaceService>();
            #endregion


            #region config Cors
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
            )
        {
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<DMLoggingMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    System.Console.WriteLine("~~~~~~~~~~~~ use1 start");

            //    using var ms = new MemoryStream();
            //    var oldBody = context.Response.Body;
            //    context.Response.Body = ms;
            //    try
            //    {
            //        await next(context);

            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //    finally
            //    {
            //        ms.Seek(0, SeekOrigin.Begin);
            //        string body = new StreamReader(ms).ReadToEnd();
            //        ms.Seek(0, SeekOrigin.Begin);
            //        await ms.CopyToAsync(oldBody);
            //        context.Response.Body = oldBody;

            //        Program.LogHttpContext(context);
            //        System.Console.WriteLine("~~~~~~~~~~~~ use1 end");
            //    }


            //});


            // 配置跨域



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Use(async (context, next) =>
            {
                System.Console.WriteLine("~~~~~~~~~~~~ use2 start");
                await next(context);
                System.Console.WriteLine("~~~~~~~~~~~~ use2 end");
            });


            app.MapControllers();





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

        public static void LogHttpContext(HttpContext context)
        {
            var connectId = context.Connection.Id;
            var localIpAddress = context.Connection.LocalIpAddress;
            var localPort = context.Connection.LocalPort;
            var clientIpAddress = context.Connection.RemoteIpAddress;
            var clientPort = context.Connection.RemotePort;
            // var 


            var requestMethod = context.Request.Method;
            var requestPath = context.Request.Path;
            var requestBody = context.Request.Body;
            var requestHeaders = context.Request.Headers;
            var requestCookies = context.Request.Cookies;
            var requestQueryString = context.Request.QueryString;


            var responseHeaders = context.Response.Headers;
            var responseCookies = context.Response.Cookies;
            var responseStatusCode = context.Response.StatusCode;
            var responseContentType = context.Response.ContentType;
            var response = context.Response.ToString();
        }

    }
}