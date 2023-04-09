using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WalletSolution.Application;
public static class DependencyInjection
{
    //public static IServiceCollection AddApplication(this IServiceCollection services)
    //{
    //    var assembly = typeof(DependencyInjection).Assembly;
    //    services.AddMediatR(config =>
    //       config.RegisterServicesFromAssembly(assembly));

    //    services.AddValidatorsFromAssembly(assembly);
    //    return services;
    //}
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
