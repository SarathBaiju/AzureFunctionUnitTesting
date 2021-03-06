using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(UserFunctionApp.Startup))]
namespace UserFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IMathFunctions, MathFunctions>();
        }
    }
}
