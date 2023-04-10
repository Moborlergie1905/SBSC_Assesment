using AutoMapper;
using WalletSolution.API.Controllers.WalletUsers.Requests;
using WalletSolution.Application.WalletUsers.Command;

namespace WalletSolution.API.AutoMapperProfiles;
public class WalletUserProfile : Profile
{
    public WalletUserProfile()
    {
        CreateMap<AddWalletUserRequest, AddUserCommand>().ReverseMap();
        CreateMap<FundOrWithrawRequest, FundWalletBalanceCommand>().ReverseMap();
        CreateMap<FundOrWithrawRequest, WithdrawWalletBalanceCommand>().ReverseMap();
        CreateMap<AddMoreCurrencyRequest, AddWalletCommand>().ReverseMap();
    }
}
