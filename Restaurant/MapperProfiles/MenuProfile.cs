using AutoMapper;
using Restaurant.Models;
using Restaurant.ViewModels;
using Restaurant.ViewModels.Manager;

namespace Restaurant.MapperProfiles;

public class MenuProfile : Profile
{
    public MenuProfile()
    {
        CreateMap<Menu, GetMenuViewModel>()
            .ReverseMap();
        CreateMap<PostMenuViewModel, Menu>()
            .ReverseMap();
        CreateMap<PutMenuViewModel, Menu>()
            .ReverseMap();
        CreateMap<PostCategoryViewModel, Category>()
            .ReverseMap();
        CreateMap<PutOrderViewModel, Order>()
            .ReverseMap();
        CreateMap<PostCartItemViewModel, CartItem>()
            .ReverseMap();
        CreateMap<PutCartItemViewModel, CartItem>()
            .ReverseMap();
        CreateMap<CartItem, GetCartItemViewModel>()
            .ReverseMap();
        CreateMap<OrderItem, GetOrderItemViewModel>()
            .ReverseMap();
        CreateMap<Order, GetOrderViewModel>()
            .ForMember(o => o.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap();
        CreateMap<CartItem, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
    }
}