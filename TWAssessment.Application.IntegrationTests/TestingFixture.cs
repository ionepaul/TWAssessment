using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TWAssessment.API;

namespace TWAssessment.Application.IntegrationTests
{
    [SetUpFixture]
    public class TestingFixture
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(_configuration);

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }
    }
}