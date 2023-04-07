using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletSolution.Common.General;
using WalletSolution.Persistence.Data;

namespace WalletSolution.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();      

        services.AddScoped((serviceProvider) =>
        {
            var option = CreateContextOptions(appOptions.ReadDatabaseConnectionString);
            return new ReadOnlyDbContext(option);
        });

        services.AddScoped((serviceProvider) =>
        {
            var option = CreateContextOptions(appOptions.WriteDatabaseConnectionString);
            return new WriteDbContext(option);
        });

        DbContextOptions<ApplicationDbContext> CreateContextOptions(string connectionString)
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                                 .UseSqlServer(connectionString)
                                 .Options;

            return contextOptions;
        }

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(appOptions.WriteDatabaseConnectionString));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
