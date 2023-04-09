using Microsoft.AspNetCore.Mvc;
using WalletSolution.APIFramework.Tools;
using WalletSolution.Application.WalletUsers.Query.QueryModels;
using WalletSolution.Application.WalletUsers.Query;

namespace WalletSolution.API.Controllers.WalletUsers;

[ApiVersion("1")]
public class TransactionsController : BaseController
{
    [HttpGet, Route("all-transactions")]
    public async Task<ApiResult<List<TransactionQueryModel>>> GetAllTransactionsAsync()
    {
        var result = await Mediator.Send(new GetTransactionListQuery());
        return new ApiResult<List<TransactionQueryModel>>(result);
    }

    [HttpGet, Route("transactions-periodic")]
    public async Task<ApiResult<List<TransactionQueryModel>>> GetPeriodicTransactionsAsync(
       [FromQuery] DateTime SelectedDate, [FromQuery] string Period)
    {
        var result = await Mediator.Send(new GetPeriodicTransactionQuery
        {            
            SelectedDate = SelectedDate,
            Period = Period
        });
        return new ApiResult<List<TransactionQueryModel>>(result);
    }

    [HttpGet, Route("user-transactions")]
    public async Task<ApiResult<List<UserTransactionQueryModel>>> GetUserTransactionsAsync([FromQuery] Guid userId)
    {
        var result = await Mediator.Send(new GetUserTransactionListQuery { UserId = userId });
        return new ApiResult<List<UserTransactionQueryModel>>(result);
    }

    [HttpGet, Route("user-transactions-periodic")]
    public async Task<ApiResult<List<UserTransactionQueryModel>>> GetUserPeriodicTransactionsAsync(
        [FromQuery] Guid userId, [FromQuery] DateTime SelectedDate, [FromQuery] string Period)
    {
        var result = await Mediator.Send(new GetUserPeriodicTransactionQuery
        {
            UserId = userId,
            SelectedDate = SelectedDate,
            Period = Period
        });
        return new ApiResult<List<UserTransactionQueryModel>>(result);
    }
}
