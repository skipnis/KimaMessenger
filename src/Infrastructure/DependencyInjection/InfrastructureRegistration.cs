using Contracts.Interfaces.Repositories;
using Contracts.Interfaces.UnitOfWork;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IChatRepository, ChatRepository>();
        
        services.AddScoped<IUserChatRepository, UserChatRepository>();
        
        services.AddScoped<IMessageRepository, MessageRepository>();

        
        return services;
    }
}