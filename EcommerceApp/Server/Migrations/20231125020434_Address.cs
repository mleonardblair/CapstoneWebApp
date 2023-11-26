using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1953babd-ddfa-421c-ae17-e1f6d44929d8"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3f8c5db8-ba7d-4e5c-b2fd-5d14e0e0a2a0"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("740d9df6-fcc8-4e4a-bb6d-0fcf6d34d05f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8368b5b2-0472-465d-8ca6-f151f0095e03"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9fe26f73-453f-45f3-aea3-0320235f1593"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ad3936b0-319e-4a4c-810d-ffcafc089b08"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ba21d039-7e3e-4e92-8d85-b8002d1f2fcf"));

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1953babd-ddfa-421c-ae17-e1f6d44929d8"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3527), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("3f8c5db8-ba7d-4e5c-b2fd-5d14e0e0a2a0"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3510), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("740d9df6-fcc8-4e4a-bb6d-0fcf6d34d05f"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3523), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("8368b5b2-0472-465d-8ca6-f151f0095e03"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3526), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("9fe26f73-453f-45f3-aea3-0320235f1593"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3524), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("ad3936b0-319e-4a4c-810d-ffcafc089b08"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3512), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("ba21d039-7e3e-4e92-8d85-b8002d1f2fcf"), new DateTime(2023, 11, 21, 18, 37, 13, 338, DateTimeKind.Utc).AddTicks(3504), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" }
                });
        }
    }
}
