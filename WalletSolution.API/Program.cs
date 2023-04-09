using WalletSolution.API;
using WalletSolution.Application;
using WalletSolution.Persistence;
using WalletSolution.Common;
using WalletSolution.Common.General;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

SiteSettings siteSetting = new SiteSettings();
siteSetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddWebApi(builder.Configuration, siteSetting);
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddCommon(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebApi(builder.Configuration);

//app.UseHttpsRedirection();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
