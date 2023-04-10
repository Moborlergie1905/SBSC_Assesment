using Microsoft.AspNetCore.Mvc;
using WalletSolution.APIFramework.Tools;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.API.Controllers.Admins.Requests;
using WalletSolution.Application.Admins.Command;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WalletSolution.API.Controllers.Admins;

[ApiVersion("1")]
public class AdminsController : BaseController
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [HttpGet, Route("all-users")]
    public async Task<ApiResult<List<WalletUserQueryModel>>> GetAsync()
    {
        //var identity = HttpContext.User.Identity as ClaimsIdentity;
        //var userClaims = identity.Claims.ToList();
        var result = await Mediator.Send(new GetWalletUserListQuery());
        return new ApiResult<List<WalletUserQueryModel>>(result);
    }

    [Authorize(Roles = "SuperAdmin, Admin")]
    [HttpPost, Route("add-currency")]
    public async Task<ApiResult<string>> AddCurrencyTypeAsync([FromBody] AddCurrencyTypeRequest request)
    {
        var command = Mapper.Map<AddCurrencyTypeRequest, AddCurrencyTypeCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("New currency type added successful");
    }

    [Authorize(Roles = "SuperAdmin, Admin")]
    [HttpPost, Route("update-currency")]
    public async Task<ApiResult<string>> UpdateCurrencyTypeAsync([FromBody] UpdateCurrencyTypeRequest request)
    {
        var command = Mapper.Map<UpdateCurrencyTypeRequest, UpdateCurrencyTypeCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("New currency type updated successful");
    }

    [Authorize(Roles = "SuperAdmin, Admin")]
    [HttpPost, Route("update-user-status")]
    public async Task<ApiResult<string>> UpdateUserStatusAsync([FromBody] UpdateUserStatusRequest request)
    {
        var command = Mapper.Map<UpdateUserStatusRequest, UpdateUserStatusCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("User status updated successful");
    }

}
