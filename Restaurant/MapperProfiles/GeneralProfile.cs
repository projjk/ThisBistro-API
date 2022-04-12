using AutoMapper;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.MapperProfiles;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<Menu, GetMenuViewModel>()
            .ReverseMap();
        CreateMap<PostCartItemViewModel, CartItem>()
            .ReverseMap();
        CreateMap<PutCartItemViewModel, CartItem>()
            .ReverseMap();
        CreateMap<CartItem, GetCartItemViewModel>()
            .ReverseMap();
        CreateMap<CartItem, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
    }
}