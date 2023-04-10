using AutoMapper;
using WalletSolution.API.Controllers.Admins.Requests;
using WalletSolution.Application.Admins.Command;

namespace WalletSolution.API.AutoMapperProfiles;
public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AddCurrencyTypeRequest, AddCurrencyTypeCommand>().ReverseMap();
        CreateMap<UpdateUserStatusRequest, UpdateUserStatusCommand>().ReverseMap();
        CreateMap<UpdateCurrencyTypeRequest, UpdateCurrencyTypeCommand>().ReverseMap();
    }
}
