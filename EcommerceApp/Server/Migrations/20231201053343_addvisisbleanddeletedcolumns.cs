using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class addvisisbleanddeletedcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GalleryImage",
                keyColumn: "Id",
                keyValue: new Guid("68481ff9-3e3d-41df-8518-65a3bcf1df55"));

            migrationBuilder.DeleteData(
                table: "GalleryImage",
                keyColumn: "Id",
                keyValue: new Guid("d0993a6c-9f3b-4be9-8f97-ae00e7dbb6d1"));

            migrationBuilder.DeleteData(
                table: "GalleryImage",
                keyColumn: "Id",
                keyValue: new Guid("d3ecb623-0bf9-4ffe-aaf2-ecfe2b4bc35e"));

            migrationBuilder.DeleteData(
                table: "GalleryImage",
                keyColumn: "Id",
                keyValue: new Guid("e3d66492-0eb0-461a-a195-aef955a8ae76"));

            migrationBuilder.DeleteData(
                table: "GalleryImage",
                keyColumn: "Id",
                keyValue: new Guid("f5591a44-9496-4028-acbc-f56d5ebbcb0a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("21d4d664-5f52-4046-8479-c814cce76a0c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("356fce6f-1981-450a-a627-a919516c396b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3980c22a-c22a-40c5-b87e-3694ad7d04fc"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("42f3b662-e785-4544-91da-d5e31773168e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6a537f7f-47f7-4d7a-88e4-84cdacb09099"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a9aa398d-7eb4-4363-9015-55c4985d1c07"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("bd8805f9-b048-47fe-aae5-d0c44c88cee8"));

            migrationBuilder.DeleteData(
                table: "Galleries",
                keyColumn: "Id",
                keyValue: new Guid("1215b4fe-a07b-4170-8b78-107124917635"));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("429922b6-76f6-42c5-8d55-8378f63aef42"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(564), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("75f37107-b795-4b83-8254-5c3becc01932"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(552), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("86e94a2f-2aef-49c7-85eb-3dc5ac4b717b"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(567), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("982915e2-9e4b-45b7-b54c-42d211d753b5"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(566), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("ad42d995-e00a-485e-b6f0-2ec59d8758f0"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(558), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("de31ef9c-9a0c-4b85-a0a0-1b6b7c89b2f6"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(561), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("fc8a181c-35cd-4558-a2ec-3139b941c375"), new DateTime(2023, 12, 1, 5, 33, 42, 924, DateTimeKind.Utc).AddTicks(559), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("429922b6-76f6-42c5-8d55-8378f63aef42"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("75f37107-b795-4b83-8254-5c3becc01932"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("86e94a2f-2aef-49c7-85eb-3dc5ac4b717b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("982915e2-9e4b-45b7-b54c-42d211d753b5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ad42d995-e00a-485e-b6f0-2ec59d8758f0"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("de31ef9c-9a0c-4b85-a0a0-1b6b7c89b2f6"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("fc8a181c-35cd-4558-a2ec-3139b941c375"));

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Galleries",
                columns: new[] { "Id", "DateModified", "Subtitle", "Title" },
                values: new object[] { new Guid("1215b4fe-a07b-4170-8b78-107124917635"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(1802), "This is where I host work and whatnot.", "Gallery" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("21d4d664-5f52-4046-8479-c814cce76a0c"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4011), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("356fce6f-1981-450a-a627-a919516c396b"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4025), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("3980c22a-c22a-40c5-b87e-3694ad7d04fc"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4024), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("42f3b662-e785-4544-91da-d5e31773168e"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4019), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("6a537f7f-47f7-4d7a-88e4-84cdacb09099"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4017), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("a9aa398d-7eb4-4363-9015-55c4985d1c07"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4020), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("bd8805f9-b048-47fe-aae5-d0c44c88cee8"), new DateTime(2023, 11, 30, 16, 27, 3, 297, DateTimeKind.Utc).AddTicks(4058), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" }
                });

            migrationBuilder.InsertData(
                table: "GalleryImage",
                columns: new[] { "Id", "GalleryId", "ImageURI" },
                values: new object[,]
                {
                    { new Guid("68481ff9-3e3d-41df-8518-65a3bcf1df55"), new Guid("1215b4fe-a07b-4170-8b78-107124917635"), "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath4.jpg" },
                    { new Guid("d0993a6c-9f3b-4be9-8f97-ae00e7dbb6d1"), new Guid("1215b4fe-a07b-4170-8b78-107124917635"), "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath5.jpg" },
                    { new Guid("d3ecb623-0bf9-4ffe-aaf2-ecfe2b4bc35e"), new Guid("1215b4fe-a07b-4170-8b78-107124917635"), "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath3.jpg" },
                    { new Guid("e3d66492-0eb0-461a-a195-aef955a8ae76"), new Guid("1215b4fe-a07b-4170-8b78-107124917635"), "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath1.jpg" },
                    { new Guid("f5591a44-9496-4028-acbc-f56d5ebbcb0a"), new Guid("1215b4fe-a07b-4170-8b78-107124917635"), "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath2.jpg" }
                });
        }
    }
}
