using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace AspnetRunBasics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) =>
                {
                    configuration
                        .Enrich.FromLogContext()   //means when we start logging we can enrich the data logged with info like machine name etc
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(
                            new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                            {
                                //Assembly.GetExecutingAssembly().GetName().Name return the name of the application or microservice the log originated from
                                //context.HostingEnvironment.EnvironmentName used to provide environment name using context object
                                IndexFormat = $"applogs-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}",
                                AutoRegisterTemplate = true,
                                NumberOfShards = 2,
                                NumberOfReplicas = 1,
                            })
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName) //ensure info on the app/microservice env from which the log is coming is also logged out
                        .ReadFrom.Configuration(context.Configuration); //means we getting the configuration from the Serilog block of appsettings.json file
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
