using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration
            .RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly));

        
        return services;
    }
}