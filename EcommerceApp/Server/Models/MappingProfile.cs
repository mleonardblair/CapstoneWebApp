using AutoMapper;
using System.Text.Json;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Newtonsoft.Json;
using EcommerceApp.Server.Models.PageModels;
using EcommerceApp.Shared.DTOs.PageDtos;

public class MappingProfile : Profile
{

    public MappingProfile()
    {

        // Product
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images ?? new string[0]));
          /*  .ForMember(dest => dest.isFavourite, opt => opt.MapFrom((src, dest, destMember, context) =>
             {
                 // Get the current user's ID
                 var currentUserId = _authService.GetUserId();
                 // Check if the current product is favorited by this user
                 return src.Favourites.Any(f => f.ApplicationUserId == currentUserId);
             }));*/
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.ImagesJson, opt => opt.MapFrom(src => src.Images == null ? "[]" : JsonConvert.SerializeObject(src.Images)))
            .ForMember(dest => dest.Images, opt => opt.Ignore());

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
        CreateMap<Favourite, FavouriteProductResponse>();
        CreateMap<FavouriteProductResponse, Favourite>();
        // Favourite
        CreateMap<Favourite, FavouriteDto>();
        CreateMap<FavouriteDto, Favourite>();
        // Cart
        CreateMap<ShoppingCart, CartDto>();
        CreateMap<CartDto, ShoppingCart>();

        // Cart Items
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartItemDto, CartItem>();

        // ProductTags
        CreateMap<ProductTag, ProductTagDto>();
        CreateMap<ProductTagDto, ProductTag>();


        /** SITE CONTENT MAPS*/
        // Gallery

        CreateMap<Gallery, GalleryDto>()
    .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src =>
        src.GalleryImages.Select(gi => gi.ImageURI).ToList()));

        CreateMap<GalleryDto, Gallery>()
            .ForMember(dest => dest.GalleryImages, opt => opt.Ignore());
        // FAQ
        CreateMap<FAQ, FAQDto>();
        CreateMap<FAQDto, FAQ>();

        // Banner
        CreateMap<Banner, BannerDto>();
        CreateMap<BannerDto, Banner>();

        // ContactUs
        CreateMap<ContactUs, ContactUsDto>();
        CreateMap<ContactUsDto, ContactUs>();

        // AboutUs
        CreateMap<AboutUs, AboutUsDto>();
        CreateMap<AboutUsDto, AboutUs>();
    }

}