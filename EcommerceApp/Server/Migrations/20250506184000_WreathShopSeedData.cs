using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class WreathShopSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Description", "Visible", "Deleted", "DateCreated", "DateModified" },
                values: new object[,]
                {
                    { new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"), "Seasonal Wreaths", "Wreaths designed for specific seasons and holidays", true, false, DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9fa9c7d-c037-4f9a-b9ba-92589a1c1dda"), "Floral Wreaths", "Wreaths made with various types of flowers, both fresh and artificial", true, false, DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d9f9c7d-c037-4f9a-b9ba-92589a1c1ddb"), "Rustic Wreaths", "Wreaths with a natural, countryside aesthetic using materials like twigs, burlap, and dried elements", true, false, DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e8673505-d6d6-4c35-a9ea-76c3c16a10a8"), "Modern Wreaths", "Contemporary wreath designs with clean lines and unique materials", true, false, DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f9fa9c7d-c037-4f9a-b9ba-92589a1c1ddc"), "Wreath Making Supplies", "Materials and tools for creating your own wreaths", true, false, DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name", "Description", "DateCreated", "DateModified" },
                values: new object[,]
                {
                    { new Guid("a1d4d664-5f52-4046-8479-c814cce76a0c"), "Spring", "Perfect for spring season decoration", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b2d4d664-5f52-4046-8479-c814cce76a0d"), "Summer", "Ideal for summer home decoration", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c3d4d664-5f52-4046-8479-c814cce76a0e"), "Fall", "Beautiful autumn-themed decorations", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d4d4d664-5f52-4046-8479-c814cce76a0f"), "Winter", "Cozy winter and holiday decorations", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e5d4d664-5f52-4046-8479-c814cce76a10"), "Christmas", "Festive Christmas holiday decorations", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), "Handmade", "Carefully crafted by hand with attention to detail", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("07d4d664-5f52-4046-8479-c814cce76a12"), "Natural", "Made with natural materials and elements", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("18d4d664-5f52-4046-8479-c814cce76a13"), "Artificial", "Created with high-quality artificial materials for longevity", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("29d4d664-5f52-4046-8479-c814cce76a14"), "Rustic", "Features a charming countryside aesthetic", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3ad4d664-5f52-4046-8479-c814cce76a15"), "Modern", "Contemporary designs with clean lines", DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "Price", "ImageURI", "ImagesJson", "Visible", "Deleted", "CategoryId", "StockQuantity", "DateCreated", "DateModified" },
                values: new object[,]
                {
                    { 
                        new Guid("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5a4b3c2d"), 
                        "Spring Blossom Wreath", 
                        "A beautiful wreath featuring spring blossoms, perfect for welcoming the spring season. Made with high-quality silk flowers that look remarkably real and will last for years to come.", 
                        59.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath1.jpg", 
                        JsonConvert.SerializeObject(new string[] 
                        { 
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath1_detail1.jpg",
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath1_detail2.jpg"
                        }), 
                        true, 
                        false, 
                        new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"), 
                        15, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("b2c3d4e5-f6a7-5b6c-0d1e-8f7g6h5i4j3"), 
                        "Summer Wildflower Wreath", 
                        "Brighten your home with this vibrant summer wildflower wreath. Features a colorful array of summer blooms arranged on a natural grapevine base.", 
                        64.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath2.jpg", 
                        JsonConvert.SerializeObject(new string[] 
                        { 
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath2_detail1.jpg",
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath2_detail2.jpg"
                        }), 
                        true, 
                        false, 
                        new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"), 
                        12, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("c3d4e5f6-a7b8-6c7d-1e2f-9g8h7i6j5k4"), 
                        "Autumn Harvest Wreath", 
                        "Celebrate the fall season with this gorgeous autumn harvest wreath. Features rich autumn colors with maple leaves, mini pumpkins, and berries on a sturdy base.", 
                        69.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath3.jpg", 
                        JsonConvert.SerializeObject(new string[] 
                        { 
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath3_detail1.jpg",
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath3_detail2.jpg"
                        }), 
                        true, 
                        false, 
                        new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"), 
                        18, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("d4e5f6a7-b8c9-7d8e-2f3g-0h1i2j3k4l5"), 
                        "Winter Wonderland Wreath", 
                        "Transform your door into a winter wonderland with this elegant wreath. Features snow-dusted pine branches, silver ornaments, and a touch of sparkle.", 
                        74.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath4.jpg", 
                        JsonConvert.SerializeObject(new string[] 
                        { 
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath4_detail1.jpg",
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath4_detail2.jpg"
                        }), 
                        true, 
                        false, 
                        new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"), 
                        20, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("e5f6a7b8-c9d0-8e9f-3g4h-1i2j3k4l5m6"), 
                        "Christmas Joy Wreath", 
                        "Spread holiday cheer with this festive Christmas wreath. Features traditional red and green colors with pine cones, berries, and a beautiful red bow.", 
                        79.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath5.jpg", 
                        JsonConvert.SerializeObject(new string[] 
                        { 
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath5_detail1.jpg",
                            "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath5_detail2.jpg"
                        }), 
                        true, 
                        false, 
                        new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"), 
                        25, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "Price", "ImageURI", "ImagesJson", "Visible", "Deleted", "CategoryId", "StockQuantity", "DateCreated", "DateModified" },
                values: new object[,]
                {
                    { 
                        new Guid("f6a7b8c9-d0e1-9f0a-4h5i-2j3k4l5m6n7"), 
                        "Lavender Dreams Wreath", 
                        "Bring the calming scent of lavender into your home with this beautiful wreath. Made with dried lavender flowers on a delicate willow base.", 
                        54.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath6.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("c9fa9c7d-c037-4f9a-b9ba-92589a1c1dda"), 
                        10, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("a7b8c9d0-e1f2-0a1b-5i6j-3k4l5m6n7o8"), 
                        "Rose Garden Wreath", 
                        "Add a touch of elegance to your home with this stunning rose garden wreath. Features an arrangement of silk roses in various shades of pink and cream.", 
                        69.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath7.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("c9fa9c7d-c037-4f9a-b9ba-92589a1c1dda"), 
                        8, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    
                    { 
                        new Guid("b8c9d0e1-f2a3-1b2c-6j7k-4l5m6n7o8p9"), 
                        "Farmhouse Burlap Wreath", 
                        "Embrace the farmhouse style with this charming burlap wreath. Features natural jute burlap with wooden accents and a touch of greenery.", 
                        49.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath8.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("d9f9c7d-c037-4f9a-b9ba-92589a1c1ddb"), 
                        15, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("c9d0e1f2-a3b4-2c3d-7k8l-5m6n7o8p9q0"), 
                        "Twig and Berry Wreath", 
                        "Add rustic charm to your home with this natural twig wreath adorned with artificial berries. Perfect for year-round display.", 
                        44.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath9.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("d9f9c7d-c037-4f9a-b9ba-92589a1c1ddb"), 
                        12, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    
                    { 
                        new Guid("d0e1f2a3-b4c5-3d4e-8l9m-6n7o8p9q0r1"), 
                        "Geometric Hoop Wreath", 
                        "A contemporary take on the traditional wreath. Features a sleek metal hoop with minimalist greenery and geometric accents.", 
                        59.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath10.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("e8673505-d6d6-4c35-a9ea-76c3c16a10a8"), 
                        10, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("e1f2a3b4-c5d6-4e5f-9m0n-7o8p9q0r1s2"), 
                        "Asymmetrical Eucalyptus Wreath", 
                        "A modern, asymmetrical design featuring preserved eucalyptus leaves. Perfect for contemporary home decor.", 
                        64.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath11.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("e8673505-d6d6-4c35-a9ea-76c3c16a10a8"), 
                        8, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    
                    { 
                        new Guid("f2a3b4c5-d6e7-5f6g-0n1o-8p9q0r1s2t3"), 
                        "Grapevine Wreath Base", 
                        "Natural grapevine wreath base for DIY projects. Perfect foundation for creating your own custom wreaths.", 
                        14.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath_base1.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("f9fa9c7d-c037-4f9a-b9ba-92589a1c1ddc"), 
                        30, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("a3b4c5d6-e7f8-6g7h-1o2p-9q0r1s2t3u4"), 
                        "Artificial Flower Assortment", 
                        "A variety pack of high-quality artificial flowers for wreath making. Includes roses, daisies, and greenery.", 
                        24.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath_flowers1.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("f9fa9c7d-c037-4f9a-b9ba-92589a1c1ddc"), 
                        20, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    },
                    { 
                        new Guid("b4c5d6e7-f8a9-7h8i-2p3q-0r1s2t3u4v5"), 
                        "Wreath Making Tool Kit", 
                        "Complete tool kit for wreath making. Includes wire cutters, floral wire, glue gun, and other essential tools.", 
                        39.99m, 
                        "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath_tools1.jpg", 
                        JsonConvert.SerializeObject(new string[] {}), 
                        true, 
                        false, 
                        new Guid("f9fa9c7d-c037-4f9a-b9ba-92589a1c1ddc"), 
                        15, 
                        DateTime.UtcNow, 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) 
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5a4b3c2d"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-5b6c-0d1e-8f7g6h5i4j3"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-6c7d-1e2f-9g8h7i6j5k4"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-7d8e-2f3g-0h1i2j3k4l5"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-8e9f-3g4h-1i2j3k4l5m6"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-9f0a-4h5i-2j3k4l5m6n7"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a7b8c9d0-e1f2-0a1b-5i6j-3k4l5m6n7o8"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b8c9d0e1-f2a3-1b2c-6j7k-4l5m6n7o8p9"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c9d0e1f2-a3b4-2c3d-7k8l-5m6n7o8p9q0"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0e1f2a3-b4c5-3d4e-8l9m-6n7o8p9q0r1"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-4e5f-9m0n-7o8p9q0r1s2"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f2a3b4c5-d6e7-5f6g-0n1o-8p9q0r1s2t3"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a3b4c5d6-e7f8-6g7h-1o2p-9q0r1s2t3u4"));
            
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b4c5d6e7-f8a9-7h8i-2p3q-0r1s2t3u4v5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a1d4d664-5f52-4046-8479-c814cce76a0c"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b2d4d664-5f52-4046-8479-c814cce76a0d"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c3d4d664-5f52-4046-8479-c814cce76a0e"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d4d4d664-5f52-4046-8479-c814cce76a0f"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e5d4d664-5f52-4046-8479-c814cce76a10"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("07d4d664-5f52-4046-8479-c814cce76a12"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("18d4d664-5f52-4046-8479-c814cce76a13"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("29d4d664-5f52-4046-8479-c814cce76a14"));
            
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3ad4d664-5f52-4046-8479-c814cce76a15"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c8673505-d6d6-4c35-a9ea-76c3c16a10a7"));
            
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c9fa9c7d-c037-4f9a-b9ba-92589a1c1dda"));
            
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d9f9c7d-c037-4f9a-b9ba-92589a1c1ddb"));
            
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e8673505-d6d6-4c35-a9ea-76c3c16a10a8"));
            
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f9fa9c7d-c037-4f9a-b9ba-92589a1c1ddc"));
        }
    }
}
