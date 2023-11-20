using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class UserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0d15632c-d7d1-4ad7-a0ef-53cea16d8cb6"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("22045126-8050-465c-8c67-0dbed75104a4"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4136f847-2b3e-4b3f-9746-1664acd5bc26"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9c5ce536-58cf-4273-9efd-065e359c5559"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a00717b1-2a69-4efa-ac73-fc98418276ae"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c29a8ee2-427b-48e7-9971-6135feef8bfa"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ee54170e-682e-422c-960c-9f31e17b4687"));

            migrationBuilder.DropColumn(
                name: "Password",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "ApplicationUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "ApplicationUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3531422a-bda9-4094-a6be-c6c54b8370ac"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8097), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("506b783f-53bb-47dc-ab6c-959b097e8b1b"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8082), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("77f656fa-531e-4b4d-896c-06659b4b1c81"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8099), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("88c65f33-1db6-460e-b590-5abae852d151"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8096), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("a0e91f43-ade2-4ccd-a776-9c730a97017e"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8089), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("af093ab7-ab5d-4a38-bea5-c8d7404606e2"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8090), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("ece66c80-efe2-4eca-942b-b9283986e1e5"), new DateTime(2023, 11, 15, 1, 40, 39, 28, DateTimeKind.Utc).AddTicks(8100), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3531422a-bda9-4094-a6be-c6c54b8370ac"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("506b783f-53bb-47dc-ab6c-959b097e8b1b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("77f656fa-531e-4b4d-896c-06659b4b1c81"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("88c65f33-1db6-460e-b590-5abae852d151"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a0e91f43-ade2-4ccd-a776-9c730a97017e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("af093ab7-ab5d-4a38-bea5-c8d7404606e2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ece66c80-efe2-4eca-942b-b9283986e1e5"));

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0d15632c-d7d1-4ad7-a0ef-53cea16d8cb6"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7385), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("22045126-8050-465c-8c67-0dbed75104a4"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7388), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("4136f847-2b3e-4b3f-9746-1664acd5bc26"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7383), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("9c5ce536-58cf-4273-9efd-065e359c5559"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7368), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("a00717b1-2a69-4efa-ac73-fc98418276ae"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7375), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("c29a8ee2-427b-48e7-9971-6135feef8bfa"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7377), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("ee54170e-682e-422c-960c-9f31e17b4687"), new DateTime(2023, 10, 30, 18, 41, 36, 878, DateTimeKind.Utc).AddTicks(7387), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" }
                });
        }
    }
}
