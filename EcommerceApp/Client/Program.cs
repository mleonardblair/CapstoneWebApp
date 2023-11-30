global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using EcommerceApp.Client;
global using EcommerceApp.Client.Services.OrderService;
global using EcommerceApp.Client.Services.FavouriteService;
global using EcommerceApp.Client.Services.ReportService;
global using EcommerceApp.Client.Services.AddressService;
global using EcommerceApp.Client.Services.CartService;
global using EcommerceApp.Client.Services.CategoryService;
global using EcommerceApp.Client.Services.ProductService;
global using EcommerceApp.Client.Services.TagService;
global using EcommerceApp.Client.Services.AuthService;
global using Azure.Storage.Blobs;
global using Blazored.LocalStorage;
global using MudBlazor;
global using MudBlazor.Services;
global using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000;
    config.SnackbarConfiguration.HideTransitionDuration = 300;
    config.SnackbarConfiguration.ShowTransitionDuration = 300;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
});
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
