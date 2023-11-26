using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class AddressAddDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "UserAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1d8b35e5-4868-49ff-addb-f0fa4def48e2"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(63), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("73be4b7e-44e7-456b-a84d-aaff96bef90d"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(60), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("7bba214d-ee01-44a0-9d04-bb5fbe8fca58"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(65), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("84eaaffe-46df-45a2-9016-4016f93cbd88"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(35), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("b24a1cd1-6ea9-47d7-96da-f35892399c13"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(58), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("b5722d41-34f3-4002-88c6-75aac65bd0c7"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(43), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("f0c2ade4-1352-4333-9796-6aa5a0a8f786"), new DateTime(2023, 11, 25, 2, 9, 1, 288, DateTimeKind.Utc).AddTicks(46), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1d8b35e5-4868-49ff-addb-f0fa4def48e2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("73be4b7e-44e7-456b-a84d-aaff96bef90d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7bba214d-ee01-44a0-9d04-bb5fbe8fca58"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("84eaaffe-46df-45a2-9016-4016f93cbd88"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b24a1cd1-6ea9-47d7-96da-f35892399c13"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b5722d41-34f3-4002-88c6-75aac65bd0c7"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f0c2ade4-1352-4333-9796-6aa5a0a8f786"));

            migrationBuilder.DropColumn(
                name: "Details",
                table: "UserAddresses");

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
    }
}
