// Startup.cs
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;

[assembly: FunctionsStartup(typeof(func.Startup))]
namespace func
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(new JsonFormatter(renderMessage: true))
                .CreateLogger();
            builder.Services.AddLogging(lb => lb.AddSerilog(logger));
        }
    }
}