using AutoMapper;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.MapperProfiles;

public class MenuProfile : Profile
{
    public MenuProfile()
    {
        CreateMap<Menu, MenuViewModel>()
            .ReverseMap();
        CreateMap<PostMenuViewModel, Menu>()
            .ReverseMap();
        CreateMap<PutMenuViewModel, Menu>()
            .ReverseMap();
        CreateMap<PostCategoryViewModel, Category>()
            .ReverseMap();
    }
}