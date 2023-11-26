global using Microsoft.AspNetCore.Components.Authorization;
global using EcommerceApp.Client.Services.AddressService;
using Azure.Storage.Blobs;
using Blazored.LocalStorage;
using EcommerceApp.Client;
using EcommerceApp.Client.Services.CartService;
using EcommerceApp.Client.Services.CategoryService;
using EcommerceApp.Client.Services.ProductService;
using EcommerceApp.Client.Services.TagService;
using EcommerceApp.Client.Services.AuthService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System;
using MudBlazor;
using EcommerceApp.Client.Services.OrderService;
using EcommerceApp.Client.Services.AddressService;

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
// Additional HttpClient for unauthenticated (public) calls
/*builder.Services.AddHttpClient("EcommerceApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Additional HttpClient for unauthenticated (public) calls
builder.Services.AddHttpClient("EcommerceApp.PublicClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));*/


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Supply HttpClient instances that include access tokens when making requests to the server project
/*builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("EcommerceApp.ServerAPI"));*/

// Optionally, register the public HttpClient, for basic shop and other page viewing for users without an account.
/*builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("EcommerceApp.PublicClient"));*/


/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });*/

await builder.Build().RunAsync();
