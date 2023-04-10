using AutoMapper;
using WalletSolution.API.Controllers;
using WalletSolution.API.Models;
using WalletSolution.Application.WalletUsers.Query;
using WalletSolution.Application.WalletUsers.Query.QueryModels;

namespace WalletSolution.API.AutoMapperProfiles;
public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<LoginRequest, LoginQuery>();
        CreateMap<UserModel, UserDto>();
    }
}
