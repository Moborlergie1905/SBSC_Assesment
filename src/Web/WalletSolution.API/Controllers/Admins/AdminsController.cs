using Microsoft.AspNetCore.Mvc;
using WalletSolution.APIFramework.Tools;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.API.Controllers.Admins.Requests;
using WalletSolution.Application.Admins.Command;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WalletSolution.API.Utilities;

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
    public async Task<ApiResult<string>> AddCurrencyTypeAsync([FromForm] AddCurrencyTypeRequest request)
    {
        if (!Helper.IsValidType(request.File))
            throw new InvalidOperationException("Invalid file type");

        string filePath = Helper.WriteFile(request.File, "Currency");

        var command = Mapper.Map<AddCurrencyTypeRequest, AddCurrencyTypeCommand>(request);
        command.CurrencyLogo = filePath;

        await Mediator.Send(command);
        return new ApiResult<string>("New currency type added successful");
    }

    [Authorize(Roles = "SuperAdmin, Admin")]
    [HttpPost, Route("update-currency")]
    public async Task<ApiResult<string>> UpdateCurrencyTypeAsync([FromBody] UpdateCurrencyTypeRequest request)
    {
        string filePath = "";
        if (request.File.Length > 0)
        {
            if (!Helper.IsValidType(request.File))
                throw new InvalidOperationException("Invalid file type");

             filePath = Helper.WriteFile(request.File, "Currency");
        }
        var command = Mapper.Map<UpdateCurrencyTypeRequest, UpdateCurrencyTypeCommand>(request);
        if (request.File.Length > 0)
            command.CurrencyLogo = filePath;

        await Mediator.Send(command);
        return new ApiResult<string>("New currency type updated successfully");
    }

    [Authorize(Roles = "SuperAdmin, Admin")]
    [HttpPost, Route("update-user-status")]
    public async Task<ApiResult<string>> UpdateUserStatusAsync([FromBody] UpdateUserStatusRequest request)
    {
        var command = Mapper.Map<UpdateUserStatusRequest, UpdateUserStatusCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("User status updated successfully");
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost, Route("Create-admin")]
    public async Task<ApiResult<string>> CreateAdminAsync([FromBody] CreateAdminRequest request)
    {
        var command = Mapper.Map<CreateAdminRequest, CreateAdminCommand>(request); 
        await Mediator.Send(command);
        return new ApiResult<string>("User created successfully");
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost, Route("assign-role")]
    public async Task<ApiResult<string>> AssignRoleAsync([FromBody] AssignRoleRequest request)
    {
        var command = Mapper.Map<AssignRoleRequest, AssignAdminRoleCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("User role assinged successfully");
    }

}
