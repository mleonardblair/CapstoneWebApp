using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class AddressRemoveType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0288580e-9486-464b-aaae-7df1077d80b3"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0481d689-2e16-4357-9b0e-33eb69b1c549"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("571865d0-e7f1-49d3-81e6-5fc73affe60b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("681832e4-5f16-4734-9e18-271ec4a3fe8c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6eb42331-0a6b-4b7b-a70e-a2715c8ee1db"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c155b6f1-5aef-4b33-be0c-91a9777575ed"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d1ec7c6b-2820-492e-9bf0-a43606bd35e0"));

            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Addresses");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1c03e9ef-4637-49ab-8b63-94e69d2b0dd3"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1802), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("2498f11d-a6ba-47c4-b3d4-4006aab94749"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1804), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("5d298faa-fbbb-4c9a-bd4d-486ec98f0f95"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1788), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("72e62c54-ab39-4af8-8488-719e9717316e"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1793), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("7317915a-cbb7-461a-bccd-c2a726b268b8"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1797), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("c998f377-7fa6-4dba-a6db-fd00bdb9c406"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1805), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("d0f64b02-43f4-45c8-8ebc-9fe75f26a55d"), new DateTime(2023, 11, 25, 2, 6, 31, 585, DateTimeKind.Utc).AddTicks(1795), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1c03e9ef-4637-49ab-8b63-94e69d2b0dd3"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2498f11d-a6ba-47c4-b3d4-4006aab94749"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5d298faa-fbbb-4c9a-bd4d-486ec98f0f95"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("72e62c54-ab39-4af8-8488-719e9717316e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7317915a-cbb7-461a-bccd-c2a726b268b8"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c998f377-7fa6-4dba-a6db-fd00bdb9c406"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d0f64b02-43f4-45c8-8ebc-9fe75f26a55d"));

            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0288580e-9486-464b-aaae-7df1077d80b3"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5361), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("0481d689-2e16-4357-9b0e-33eb69b1c549"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5455), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("571865d0-e7f1-49d3-81e6-5fc73affe60b"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5448), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("681832e4-5f16-4734-9e18-271ec4a3fe8c"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5447), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("6eb42331-0a6b-4b7b-a70e-a2715c8ee1db"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5433), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("c155b6f1-5aef-4b33-be0c-91a9777575ed"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5450), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("d1ec7c6b-2820-492e-9bf0-a43606bd35e0"), new DateTime(2023, 11, 25, 2, 4, 34, 426, DateTimeKind.Utc).AddTicks(5451), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" }
                });
        }
    }
}
