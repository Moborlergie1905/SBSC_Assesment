using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletSolution.API.Models;
using WalletSolution.API.Utilities;
using WalletSolution.APIFramework.Tools;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Common.General;

namespace WalletSolution.API.Controllers;

[ApiVersion("1")]
public class AccountController : BaseController
{
    [AllowAnonymous]
    [HttpPost, Route("login")]
    public async Task<ApiResult<string>> Login([FromBody] LoginRequest request)
    {
        var setting = SiteSettings.JwtSettings;       

        var command = Mapper.Map<LoginRequest, LoginQuery>(request);
        var result = await Mediator.Send(command);
        var userModel = Mapper.Map<UserModel, UserDto>(result);
        string token = Helper.GenerateToken(userModel, setting);
        return new ApiResult<string>(token);
    }
}
