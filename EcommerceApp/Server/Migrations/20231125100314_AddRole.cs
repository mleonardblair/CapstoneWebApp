using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class AddRole : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2b779732-bb2a-40c5-be05-0370a3c2809a"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(238), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("5f7c241c-d245-477c-966b-e5b980ed4d02"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(241), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("b2a7280a-a70d-4815-b45b-fa626ad97420"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(245), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("bd6fc7b1-3ab0-46cf-b92b-5836f4e5b24e"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(236), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("c1993d2c-de1f-4c28-b249-ee3216b30dc3"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(239), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("cb65fd6c-6245-4304-b135-4955a67c6b41"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(244), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("ce03a8b1-8097-4aa6-b9f6-1f67a681243c"), new DateTime(2023, 11, 25, 10, 3, 14, 657, DateTimeKind.Utc).AddTicks(219), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2b779732-bb2a-40c5-be05-0370a3c2809a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5f7c241c-d245-477c-966b-e5b980ed4d02"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b2a7280a-a70d-4815-b45b-fa626ad97420"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("bd6fc7b1-3ab0-46cf-b92b-5836f4e5b24e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c1993d2c-de1f-4c28-b249-ee3216b30dc3"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cb65fd6c-6245-4304-b135-4955a67c6b41"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ce03a8b1-8097-4aa6-b9f6-1f67a681243c"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ApplicationUsers");

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
