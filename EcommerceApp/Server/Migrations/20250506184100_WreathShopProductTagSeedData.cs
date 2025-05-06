using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class WreathShopProductTagSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductTags",
                columns: new[] { "Id", "ProductId", "TagId", "DateCreated", "DateModified" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f"), new Guid("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5a4b3c2d"), new Guid("a1d4d664-5f52-4046-8479-c814cce76a0c"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Spring
                    { new Guid("d2e3f4a5-b6c7-8d9e-0f1a-2b3c4d5e6f7a"), new Guid("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5a4b3c2d"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    { new Guid("e3f4a5b6-c7d8-9e0f-1a2b-3c4d5e6f7a8b"), new Guid("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5a4b3c2d"), new Guid("18d4d664-5f52-4046-8479-c814cce76a13"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Artificial
                    
                    { new Guid("f4a5b6c7-d8e9-0f1a-2b3c-4d5e6f7a8b9c"), new Guid("b2c3d4e5-f6a7-5b6c-0d1e-8f7g6h5i4j3"), new Guid("b2d4d664-5f52-4046-8479-c814cce76a0d"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Summer
                    { new Guid("a5b6c7d8-e9f0-1a2b-3c4d-5e6f7a8b9c0d"), new Guid("b2c3d4e5-f6a7-5b6c-0d1e-8f7g6h5i4j3"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    { new Guid("b6c7d8e9-f0a1-2b3c-4d5e-6f7a8b9c0d1e"), new Guid("b2c3d4e5-f6a7-5b6c-0d1e-8f7g6h5i4j3"), new Guid("07d4d664-5f52-4046-8479-c814cce76a12"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Natural
                    
                    { new Guid("c7d8e9f0-a1b2-3c4d-5e6f-7a8b9c0d1e2f"), new Guid("c3d4e5f6-a7b8-6c7d-1e2f-9g8h7i6j5k4"), new Guid("c3d4d664-5f52-4046-8479-c814cce76a0e"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Fall
                    { new Guid("d8e9f0a1-b2c3-4d5e-6f7a-8b9c0d1e2f3a"), new Guid("c3d4e5f6-a7b8-6c7d-1e2f-9g8h7i6j5k4"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    { new Guid("e9f0a1b2-c3d4-5e6f-7a8b-9c0d1e2f3a4b"), new Guid("c3d4e5f6-a7b8-6c7d-1e2f-9g8h7i6j5k4"), new Guid("29d4d664-5f52-4046-8479-c814cce76a14"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Rustic
                    
                    { new Guid("f0a1b2c3-d4e5-6f7a-8b9c-0d1e2f3a4b5c"), new Guid("d4e5f6a7-b8c9-7d8e-2f3g-0h1i2j3k4l5"), new Guid("d4d4d664-5f52-4046-8479-c814cce76a0f"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Winter
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), new Guid("d4e5f6a7-b8c9-7d8e-2f3g-0h1i2j3k4l5"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    { new Guid("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"), new Guid("d4e5f6a7-b8c9-7d8e-2f3g-0h1i2j3k4l5"), new Guid("18d4d664-5f52-4046-8479-c814cce76a13"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Artificial
                    
                    { new Guid("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"), new Guid("e5f6a7b8-c9d0-8e9f-3g4h-1i2j3k4l5m6"), new Guid("e5d4d664-5f52-4046-8479-c814cce76a10"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Christmas
                    { new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"), new Guid("e5f6a7b8-c9d0-8e9f-3g4h-1i2j3k4l5m6"), new Guid("d4d4d664-5f52-4046-8479-c814cce76a0f"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Winter
                    { new Guid("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"), new Guid("e5f6a7b8-c9d0-8e9f-3g4h-1i2j3k4l5m6"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    
                    { new Guid("f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"), new Guid("f6a7b8c9-d0e1-9f0a-4h5i-2j3k4l5m6n7"), new Guid("a1d4d664-5f52-4046-8479-c814cce76a0c"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Spring
                    { new Guid("a7b8c9d0-e1f2-3a4b-5c6d-7e8f9a0b1c2d"), new Guid("f6a7b8c9-d0e1-9f0a-4h5i-2j3k4l5m6n7"), new Guid("07d4d664-5f52-4046-8479-c814cce76a12"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Natural
                    
                    { new Guid("b8c9d0e1-f2a3-4b5c-6d7e-8f9a0b1c2d3e"), new Guid("a7b8c9d0-e1f2-0a1b-5i6j-3k4l5m6n7o8"), new Guid("b2d4d664-5f52-4046-8479-c814cce76a0d"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Summer
                    { new Guid("c9d0e1f2-a3b4-5c6d-7e8f-9a0b1c2d3e4f"), new Guid("a7b8c9d0-e1f2-0a1b-5i6j-3k4l5m6n7o8"), new Guid("18d4d664-5f52-4046-8479-c814cce76a13"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Artificial
                    
                    { new Guid("d0e1f2a3-b4c5-6d7e-8f9a-0b1c2d3e4f5a"), new Guid("b8c9d0e1-f2a3-1b2c-6j7k-4l5m6n7o8p9"), new Guid("29d4d664-5f52-4046-8479-c814cce76a14"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Rustic
                    { new Guid("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"), new Guid("b8c9d0e1-f2a3-1b2c-6j7k-4l5m6n7o8p9"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    
                    { new Guid("f2a3b4c5-d6e7-8f9a-0b1c-2d3e4f5a6b7c"), new Guid("c9d0e1f2-a3b4-2c3d-7k8l-5m6n7o8p9q0"), new Guid("29d4d664-5f52-4046-8479-c814cce76a14"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Rustic
                    { new Guid("a3b4c5d6-e7f8-9a0b-1c2d-3e4f5a6b7c8d"), new Guid("c9d0e1f2-a3b4-2c3d-7k8l-5m6n7o8p9q0"), new Guid("07d4d664-5f52-4046-8479-c814cce76a12"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Natural
                    
                    { new Guid("b4c5d6e7-f8a9-0b1c-2d3e-4f5a6b7c8d9e"), new Guid("d0e1f2a3-b4c5-3d4e-8l9m-6n7o8p9q0r1"), new Guid("3ad4d664-5f52-4046-8479-c814cce76a15"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Modern
                    { new Guid("c5d6e7f8-a9b0-1c2d-3e4f-5a6b7c8d9e0f"), new Guid("d0e1f2a3-b4c5-3d4e-8l9m-6n7o8p9q0r1"), new Guid("f6d4d664-5f52-4046-8479-c814cce76a11"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Handmade
                    
                    { new Guid("d6e7f8a9-b0c1-2d3e-4f5a-6b7c8d9e0f1a"), new Guid("e1f2a3b4-c5d6-4e5f-9m0n-7o8p9q0r1s2"), new Guid("3ad4d664-5f52-4046-8479-c814cce76a15"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }, // Modern
                    { new Guid("e7f8a9b0-c1d2-3e4f-5a6b-7c8d9e0f1a2b"), new Guid("e1f2a3b4-c5d6-4e5f-9m0n-7o8p9q0r1s2"), new Guid("07d4d664-5f52-4046-8479-c814cce76a12"), DateTime.UtcNow, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) } // Natural
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("d2e3f4a5-b6c7-8d9e-0f1a-2b3c4d5e6f7a"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("e3f4a5b6-c7d8-9e0f-1a2b-3c4d5e6f7a8b"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("f4a5b6c7-d8e9-0f1a-2b3c-4d5e6f7a8b9c"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("a5b6c7d8-e9f0-1a2b-3c4d-5e6f7a8b9c0d"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("b6c7d8e9-f0a1-2b3c-4d5e-6f7a8b9c0d1e"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("c7d8e9f0-a1b2-3c4d-5e6f-7a8b9c0d1e2f"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("d8e9f0a1-b2c3-4d5e-6f7a-8b9c0d1e2f3a"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("e9f0a1b2-c3d4-5e6f-7a8b-9c0d1e2f3a4b"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("f0a1b2c3-d4e5-6f7a-8b9c-0d1e2f3a4b5c"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("a7b8c9d0-e1f2-3a4b-5c6d-7e8f9a0b1c2d"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("b8c9d0e1-f2a3-4b5c-6d7e-8f9a0b1c2d3e"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("c9d0e1f2-a3b4-5c6d-7e8f-9a0b1c2d3e4f"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("d0e1f2a3-b4c5-6d7e-8f9a-0b1c2d3e4f5a"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("f2a3b4c5-d6e7-8f9a-0b1c-2d3e4f5a6b7c"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("a3b4c5d6-e7f8-9a0b-1c2d-3e4f5a6b7c8d"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("b4c5d6e7-f8a9-0b1c-2d3e-4f5a6b7c8d9e"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("c5d6e7f8-a9b0-1c2d-3e4f-5a6b7c8d9e0f"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("d6e7f8a9-b0c1-2d3e-4f5a-6b7c8d9e0f1a"));
            
            migrationBuilder.DeleteData(
                table: "ProductTags",
                keyColumn: "Id",
                keyValue: new Guid("e7f8a9b0-c1d2-3e4f-5a6b-7c8d9e0f1a2b"));
        }
    }
}
