using Azure.Storage.Blobs;
using EcommerceApp.Client;
using EcommerceApp.Client.Services.CategoryService;
using EcommerceApp.Client.Services.ProductService;
using EcommerceApp.Client.Services.TagService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// Additional HttpClient for unauthenticated (public) calls
builder.Services.AddHttpClient("EcommerceApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Additional HttpClient for unauthenticated (public) calls
builder.Services.AddHttpClient("EcommerceApp.PublicClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));



builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagService, TagService>();


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("EcommerceApp.ServerAPI"));

// Optionally, register the public HttpClient, for basic shop and other page viewing for users without an account.
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("EcommerceApp.PublicClient"));


/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });*/

await builder.Build().RunAsync();
