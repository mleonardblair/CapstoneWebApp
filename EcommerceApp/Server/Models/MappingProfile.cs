using AutoMapper;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();

        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        // AppUser
        CreateMap<AppUserDto, ApplicationUser>();
        CreateMap<ApplicationUser, AppUserDto>();

        // Report
        CreateMap<Report, ReportDto>();
        CreateMap<ReportDto, Report>();

        // Tag
        CreateMap<Tag, TagDto>();
        CreateMap<TagDto, Tag>();

        // Address
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();


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




    }
}