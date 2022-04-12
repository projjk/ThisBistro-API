using AutoMapper;
using Restaurant.Models;
using Restaurant.ViewModels.Manager;

namespace Restaurant.MapperProfiles;

public class ManagerProfile : Profile
{
    public ManagerProfile()
    {
        CreateMap<Menu, GetMenuViewModel>()
            .ReverseMap();
        CreateMap<PostMenuViewModel, Menu>().ReverseMap();
        CreateMap<PutMenuViewModel, Menu>()
            .ReverseMap();
        CreateMap<PostCategoryViewModel, Category>()
            .ReverseMap();
        CreateMap<PutOrderViewModel, Order>()
            .ReverseMap();
        CreateMap<OrderItem, GetOrderItemViewModel>()
            .ReverseMap();
        CreateMap<Order, GetOrderViewModel>()
            .ForMember(o => o.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap();

    }
}