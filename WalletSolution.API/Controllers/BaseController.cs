using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletSolution.APIFramework.Attributes;
using WalletSolution.Common.General;

namespace WalletSolution.API.Controllers;

//[Authorize]
[ValidateModelState]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected IServiceProvider Resolver => HttpContext.RequestServices;
    protected T GetService<T>()
    {
        return Resolver.GetService<T>();
    }

    protected IMapper Mapper => GetService<IMapper>();

    protected IMediator Mediator => GetService<IMediator>();

    protected ILogger Logger => GetService<ILogger>();
    protected IConfiguration Configuration => GetService<IConfiguration>();
    protected SiteSettings  SiteSettings => GetService<SiteSettings>();
}
