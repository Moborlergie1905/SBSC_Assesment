using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using WalletSolution.APIFramework.Attributes;
using WalletSolution.APIFramework.Swagger;
using WalletSolution.Common.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WalletSolution.API.Filters;
using WalletSolution.Common;
using ApplicationException = WalletSolution.Common.ApplicationException;
using FluentValidation.AspNetCore;
using WalletSolution.Common.Behaviours;

namespace WalletSolution.API;
public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSettings)
    {
        services.Configure<SiteSettings>(configuration.GetSection(nameof(SiteSettings)));
        var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();       

        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddSwaggerOptions();
        services.AddHttpContextAccessor();
        //services.AddCustomIdentity(siteSettings.IdentitySettings);
        services.AddJwtAuthentication(siteSettings.JwtSettings);
        services.AddApiControllers();
        services.AddAutoMapperConfiguration();
        services.AddMediatorConfiguration();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());   
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddSingleton<SiteSettings>(siteSettings);

        return services;
    }

    public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        app.UseAppSwagger(configuration);
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();           
        });

        return app;
    }  
    public static IServiceCollection AddSwaggerOptions(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
           // options.EnableAnnotations();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Wallet.WebUI",
                Description = "Wallet Web Api",
                Contact = new OpenApiContact
                {
                    Name = "IdealSoft Software Solutions",
                    Email = "moborlergie1905@gmail.com",
                    Url = new Uri("https://github.com/moborlergie1905"),
                },
            });
           
            options.OperationFilter<ApplySummariesOperationFilter>();

            //Add 401 response and security requirements (Lock icon) to actions that need authorization
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             Array.Empty<string>()
                     }
                 });

            #region Versioning

            options.OperationFilter<RemoveVersionParameters>();

            options.DocumentFilter<SetVersionInPaths>();

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v}" == docName);
            });           
        });

        return services;
    }

    public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "SMS.WebUI v1");            

            options.DocExpansion(DocExpansion.None);
        });

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        app.UseReDoc(options =>
        {
            options.SpecUrl("/swagger/v1/swagger.json");            

            #region Customizing
            //By default, the ReDoc UI will be exposed at "/api-docs"
            //options.RoutePrefix = "docs";
            //options.DocumentTitle = "My API Docs";

            options.EnableUntrustedSpec();
            options.ScrollYOffset(10);
            options.HideHostname();
            options.HideDownloadButton();
            options.ExpandResponses("200,201");
            options.RequiredPropsFirst();
            options.NoAutoAuth();
            options.PathInMiddlePanel();
            options.HideLoading();
            options.NativeScrollbars();
            options.DisableSearch();
            options.OnlyRequiredInSamples();
            options.SortPropsAlphabetically();
            #endregion
        });

        return app;
    }
    #endregion

    public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,          
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidAudience = jwtSettings.Audience,                
                ValidIssuer = jwtSettings.Issuer,
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;

            //options.Events = new JwtBearerEvents
            //{
            //    OnAuthenticationFailed = context =>
            //    {
            //        if (context.Exception != null)
            //            throw new ApplicationException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

            //        return Task.CompletedTask;
            //    },
            //    OnTokenValidated = async context =>
            //    {
            //       // var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    

            //        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            //        if (claimsIdentity.Claims?.Any() != true)
            //            context.Fail("This token has no claims.");                   

            //        //Find user and token from database and perform your custom validation
            //        /*var userId = claimsIdentity.GetUserId<int>();
            //        var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId); */             

            //      /*  if (!user.IsActive)
            //            context.Fail("User is not active.");*/

            //        //await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
            //    },
            //    OnChallenge = context =>
            //    {
            //        if (context.AuthenticateFailure != null)
            //            throw new ApplicationException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
            //        throw new ApplicationException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
            //    }
            //};
        });
    }

    public static void AddApiControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));
            options.Filters.Add(new ApiExceptionFilter());
        })
        .AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<Program>();
        });

        services.AddCors();
    }

    //public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
    //{
    //    services.AddIdentity<User, Role>(identityOptions =>
    //    {
    //        //Password Settings
    //        identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
    //        identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
    //        identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric; //#@!
    //        identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
    //        identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

    //        //UserName Settings
    //        identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
    //    })
    //    .AddEntityFrameworkStores<ApplicationDbContext>()
    //    .AddDefaultTokenProviders();
    //}

    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
    }
    public static void AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Mediator>());
    }
}
