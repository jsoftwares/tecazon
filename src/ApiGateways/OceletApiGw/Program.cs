using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


/**We follow routing operations in Ocelot by checking the log, so we will setup/extend/manage d log here in other to see 
 * our console and debug window**/
builder.Host.ConfigureLogging((hostingContext, loggingBuilder) => 
{
    //this is saying check for d log operations in appsettings.json "Logging" section
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();    //important so that when Ocelot receives/route any request, we see it logged in console
    loggingBuilder.AddDebug();  //we also want to track logs from debug window
});

//Add Ocelot related objects into AsP.Net builtin dependency injection & adding d required services into d ASP.Net service collection
builder.Services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Configure pipelines - middleware for requests
/**When any request comes into this ASP.Net application, d request goes through UseOcelot middleware & d middleware then checks d
 * custom json configurations which tells it how to route the incoming request **/
await app.UseOcelot();

app.Run();
