using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletSolution.APIFramework.Attributes;

namespace WalletSolution.API.Controllers;

[ValidateModelState]
//[Route("api/[controller]")]
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
}
