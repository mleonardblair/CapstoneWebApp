using EcommerceApp.Client.Pages;
using EcommerceApp.Server.Models;
using EcommerceApp.Server.Models.PageModels;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        #nullable disable
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        // Add other DbSet properties for your other models
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Models.PageModels.Gallery> Galleries { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Banner> Banners { get; set; }

        #nullable restore
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Banner>()
                .HasKey(b => b.Id);
                

            modelBuilder.Entity<Banner>()
                .Property(b => b.BannerImageURI)
                .IsRequired();

            modelBuilder.Entity<AboutUs>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<ContactUs>()
                 .HasKey(c => c.Id);

            modelBuilder.Entity<FAQ>()
               .HasMany(f => f.Questions)
               .WithOne(q => q.FAQ)
               .HasForeignKey(q => q.FAQId);

            modelBuilder.Entity<FAQ>()
                .HasMany(f => f.Answers)
                .WithOne(a => a.FAQ)
                .HasForeignKey(a => a.FAQId);

            modelBuilder.Entity<Models.PageModels.Gallery>()
                .HasMany(g => g.GalleryImages)
                .WithOne(i => i.Gallery)
                .HasForeignKey(i => i.GalleryId);
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(u => u.ShoppingCart)
                      .WithOne(s => s.ApplicationUser)
                      .HasForeignKey<ShoppingCart>(s => s.ApplicationUserId);
            });

            // ProductTag Configuration
            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => new { pt.ProductId, pt.TagId });

            modelBuilder.Entity<Tag>()
                .HasData(
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ornament",
                        Description = "Outdoor and indoor ornaments that add flair to living spaces and gardens.",
                    },
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Garden",
                        Description = "Items related to garden decorations and maintenance, including plants, tools, and accessories."
                    },
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Wreaths",
                        Description = "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions."
                    },
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Macrame",
                        Description = "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more."
                    },
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Calligraphy",
                        Description = "The art of beautiful handwriting, often used in invitations, letters, and decorative texts."
                    },
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Embroidery",
                        Description = "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks."
                    },
                    new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "Decorative",
                        Description = "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function."
                    }
                );


            // User Configuration
            modelBuilder.Entity<ApplicationUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Address Configuration
            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Address>()
                .Property(a => a.AddressLine)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Address>()
                .Property(a => a.Province)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.PostalCode)
                .HasMaxLength(50);

            // UserAddress Configuration
            modelBuilder.Entity<UserAddress>()
                .HasKey(ua => ua.Id);

            modelBuilder.Entity<UserAddress>()
                .HasOne(ua => ua.ApplicationUser)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.ApplicationUserId);

            modelBuilder.Entity<UserAddress>()
                .HasOne(ua => ua.Address)
                .WithMany(a => a.UserAddresses) // Assuming Address entity has a collection property named UserAddresses
                .HasForeignKey(ua => ua.AddressId);

            modelBuilder.Entity<UserAddress>()
                .Property(ua => ua.AddressType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);
                

            // Favourite Configuration
            modelBuilder.Entity<Favourite>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.ApplicationUser)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.ApplicationUserId);


            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Favourites)
                .HasForeignKey(f => f.ProductId);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);


            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);

            // Tag Configuration
            modelBuilder.Entity<Tag>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Description)
                .HasMaxLength(1000);

            // Category Configuration
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(1000);

            // Product Configuration
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Order Configuration
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ApplicationUserId);

            modelBuilder.Entity<Order>()
                .Property(o => o.Discount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.Tax)
                .HasColumnType("decimal(18,2)");


            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);


            // CartItem Configuration
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.Id);

            modelBuilder.Entity<CartItem>()
                    .HasOne(ci => ci.ShoppingCart) // Indicates CartItem has one ShoppingCart
                    .WithMany(sc => sc.CartItems)  // Indicates ShoppingCart has many CartItems
                    .HasForeignKey(ci => ci.ShoppingCartId) // Specifies the foreign key
                    .OnDelete(DeleteBehavior.Cascade);  // Configures cascade delete behavior

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);

            // Payment Configuration
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);

            // Review Configuration
            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ApplicationUserId);
                

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);


            // Report Configuration
            modelBuilder.Entity<Report>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);  // Set no action on delete

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Order)
                .WithMany(o => o.Reports) // Assuming Orders entity has a collection property named Reports
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.NoAction);  // Set no action on delete


        }

    }

}
