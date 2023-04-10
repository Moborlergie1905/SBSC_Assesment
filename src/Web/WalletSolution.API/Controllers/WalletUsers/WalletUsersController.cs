using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WalletSolution.API.Controllers.WalletUsers.Requests;
using WalletSolution.API.Utilities;
using WalletSolution.APIFramework.Tools;
using WalletSolution.Application.WalletUsers.Command;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.API.Controllers.WalletUsers;

[ApiVersion("1")]
public class WalletUsersController : BaseController
{

    [Authorize(Roles = "SuperAdmin, Admin, WalletUser")]
    [HttpGet, Route("single-id")]
    public async Task<ApiResult<WalletUserQueryModel>> GetByIdAsync([FromQuery] Guid id)
    {
        var result = await Mediator.Send(new GetWalletUserByIdQuery { Id = id });
        return new ApiResult<WalletUserQueryModel>(result);
    }
    [Authorize(Roles = "SuperAdmin, Admin, WalletUser")]
    [HttpGet, Route("single-email")]
    public async Task<ApiResult<WalletUserQueryModel>> GetByEmailAsync([FromQuery] string email)
    {
        var result = await Mediator.Send(new GetWalletUserByEmailQuery { Email = email });
        return new ApiResult<WalletUserQueryModel>(result);
    }

    [Authorize(Roles = "SuperAdmin, Admin, WalletUser")]
    [HttpGet, Route("user-wallet")]
    public async Task<ApiResult<List<WalletQueryModel>>> GetUserWalletsAsync([FromQuery] Guid id)
    {
        var result = await Mediator.Send(new GetUserWalletListQuery { UserId = id });
        return new ApiResult<List<WalletQueryModel>>(result);
    }

    [AllowAnonymous]
    [HttpPost, Route("sign-up")]
    public async Task<ApiResult<string>> AddUserAsync([FromForm] AddWalletUserRequest request)
    {
        if (!Helper.IsValidType(request.File))
            throw new InvalidOperationException("Invalid file type");

        string filePath = Helper.WriteFile(request.File);

        var command = Mapper.Map<AddWalletUserRequest, AddUserCommand>(request);
        command.ProfilePicture = filePath;
        await Mediator.Send(command);
        return new ApiResult<string>("Sign-up was successful");
    }

    [Authorize(Roles = "WalletUser")]
    [HttpPost, Route("upload-profile-picture")]
    public async Task<ApiResult<string>> UploadProfilePictureAsync(IFormFile file)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var claims = identity.Claims;
        string email = claims.FirstOrDefault(t => t.Type == ClaimTypes.Email).Value;

        if (!Helper.IsValidType(file))
            throw new InvalidOperationException("Invalid file type");

        string filePath = Helper.WriteFile(file);
        var command = new UploadProfilePictureCommand
        {
            Email = email,
            ProfilePicture = filePath
        };
        await Mediator.Send(command);
        return new ApiResult<string>("profile picture uploaded successfully");
    }

    [Authorize(Roles = "WalletUser")]
    [HttpPost, Route("fund-wallet")]
    public async Task<ApiResult<string>> FundWalletAsync([FromBody] FundOrWithrawRequest request)
    {
        var command = Mapper.Map<FundOrWithrawRequest, FundWalletBalanceCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("Transaction was successful");
    }

    [Authorize(Roles = "WalletUser")]
    [HttpPost, Route("withraw-wallet")]
    public async Task<ApiResult<string>> WithdrawWalletAsync([FromBody] FundOrWithrawRequest request)
    {
        var command = Mapper.Map<FundOrWithrawRequest, WithdrawWalletBalanceCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("Transaction was successful");
    }

    [Authorize(Roles = "WalletUser")]
    [HttpPost, Route("new-wallet-currency")]
    public async Task<ApiResult<string>> AddMoreCurrencyToWalletAsync([FromBody] AddMoreCurrencyRequest request)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var claims = identity.Claims;
        string email = claims.FirstOrDefault(t => t.Type == ClaimTypes.Email).Value;
        var command = Mapper.Map<AddMoreCurrencyRequest, AddWalletCommand>(request);
        command.Email = email;
        await Mediator.Send(command);
        return new ApiResult<string>("New currency added to your wallet");
    }
}
