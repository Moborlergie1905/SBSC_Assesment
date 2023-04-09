﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet, Route("single-id")]
    public async Task<ApiResult<WalletUserQueryModel>> GetByIdAsync([FromQuery] Guid id)
    {
        var result = await Mediator.Send(new GetWalletUserByIdQuery { Id = id });
        return new ApiResult<WalletUserQueryModel>(result);
    }
    [HttpGet, Route("single-email")]
    public async Task<ApiResult<WalletUserQueryModel>> GetByEmailAsync([FromQuery] string email)
    {
        var result = await Mediator.Send(new GetWalletUserByEmailQuery { Email = email });
        return new ApiResult<WalletUserQueryModel>(result);
    }
   

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
        var command = Mapper.Map<AddWalletUserRequest, AddUserCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("Sign-up was successful");
    }

    [HttpPost, Route("fund-wallet")]
    public async Task<ApiResult<string>> FundWalletAsync([FromBody] FundOrWithrawRequest request)
    {
        var command = Mapper.Map<FundOrWithrawRequest, FundWalletBalanceCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("Transaction was successful");
    }

    [HttpPost, Route("withraw-wallet")]
    public async Task<ApiResult<string>> WithdrawWalletAsync([FromBody] FundOrWithrawRequest request)
    {
        var command = Mapper.Map<FundOrWithrawRequest, WithdrawWalletBalanceCommand>(request);
        await Mediator.Send(command);
        return new ApiResult<string>("Transaction was successful");
    }   
}
