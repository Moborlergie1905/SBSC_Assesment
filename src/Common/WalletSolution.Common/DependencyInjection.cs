using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WalletSolution.Common;
public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
