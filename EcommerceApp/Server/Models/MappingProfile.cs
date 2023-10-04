using AutoMapper;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        CreateMap<AppUserDto, ApplicationUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Map Email to UserName


        CreateMap<ApplicationUser, AppUserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName)); // Map UserName to Email
                                                                            // New mapping for Report and ReportDto
        CreateMap<Report, ReportDto>();
        CreateMap<ReportDto, Report>();

        // Tag
        CreateMap<Tag, TagDto>();
        CreateMap<TagDto, Tag>();

        // Address
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());


        // Order
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();

        // OrderItem
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItemDto, OrderItem>();

        // Favourite
        CreateMap<Favourite, FavouriteDto>();
        CreateMap<FavouriteDto, Favourite>();

        // Cart
        CreateMap<ShoppingCart, CartDto>();
        CreateMap<CartDto, ShoppingCart>();

        // Cart Items
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartItemDto, CartItem>();

        // Report
        CreateMap<Report, ReportDto>();
        CreateMap<ReportDto, Report>();


    }
}