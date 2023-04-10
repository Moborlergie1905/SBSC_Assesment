using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletSolution.Common.General;
using WalletSolution.Persistence;
using WalletSolution.Persistence.BackgroundService;

var builder = WebApplication.CreateBuilder(args);
var appOptions = builder.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();

builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                           .UseSimpleAssemblyNameTypeSerializer()
                                           .UseDefaultTypeSerializer()
                                           .UseSqlServerStorage(appOptions?.WriteDatabaseConnectionString));
builder.Services.AddHangfireServer();

builder.Services.AddSingleton<SimpleInterest>();

var provider = builder.Services.BuildServiceProvider();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHangfireDashboard();

IRecurringJobManager recurringJobManager = new RecurringJobManager();

recurringJobManager.AddOrUpdate(
"Run At 12 Mid-Night",
            () => provider.GetService<SimpleInterest>().CalculateInterest(),
            Cron.Daily(0));

app.Run();
