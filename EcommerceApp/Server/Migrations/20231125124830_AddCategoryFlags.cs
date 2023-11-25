using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class AddCategoryFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1497de30-f97e-4b4e-8dc2-df9d1db416ce"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7870), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("4477cd92-8691-4d5c-af16-d5455295c1a2"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7887), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("78d562da-0149-4348-b68d-7486b99215d4"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7886), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("90dd64c3-c7dd-4fe2-9c89-237600beedf0"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7863), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("9106a695-00a0-49e0-8e2d-cd2ae9271c3a"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7883), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("bb8400ca-b9d7-4fc1-b996-cfaef82e811a"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7869), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("c4aa03dc-83b0-448b-b9db-a459f3717109"), new DateTime(2023, 11, 25, 12, 48, 30, 471, DateTimeKind.Utc).AddTicks(7884), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1497de30-f97e-4b4e-8dc2-df9d1db416ce"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4477cd92-8691-4d5c-af16-d5455295c1a2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("78d562da-0149-4348-b68d-7486b99215d4"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("90dd64c3-c7dd-4fe2-9c89-237600beedf0"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9106a695-00a0-49e0-8e2d-cd2ae9271c3a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("bb8400ca-b9d7-4fc1-b996-cfaef82e811a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c4aa03dc-83b0-448b-b9db-a459f3717109"));

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Categories");

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
    }
}
