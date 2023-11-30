global using EcommerceApp.Server.Services.OrderService;
global using EcommerceApp.Server.Services.FavouriteService;
global using EcommerceApp.Server.Services.PaymentService;
global using EcommerceApp.Server.Services.CartService;
global using EcommerceApp.Server.Services.ReportService;
global using EcommerceApp.Server.Services.AuthService;
global using EcommerceApp.Server.Services.AddressService;
global using EcommerceApp.Server.Services.TagService;
global using EcommerceApp.Server.Services.ProductTagService;
global using EcommerceApp.Server.Services.CategoryService;
global using EcommerceApp.Server.Services.ProductService;
global using EcommerceApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AzureDBConnection");
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyEcommerceApplication API V1", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, options => options.CommandTimeout(120)));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductTagService, ProductTagService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = 
             new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
             ValidateIssuer = false,
             ValidateAudience = false,
         };
     });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
    app.UseSwagger();  // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    });


}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
