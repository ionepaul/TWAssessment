using Microsoft.Extensions.DependencyInjection;
using TWAssessment.Application.Common.Interfaces;
using TWAssessment.Infrastructure.Services;

namespace TWAssessment.Infrastructure
{
    /// <summary>
    /// Infrastructure layer extension for registering into the pipeline
    /// </summary>
    public static class InfrastructureServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IMockDataService, MockDataService>();

            return services;
        }
    }
}
