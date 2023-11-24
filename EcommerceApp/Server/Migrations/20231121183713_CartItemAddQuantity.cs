using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class CartItemAddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3e231fd5-c961-4c0b-8910-5b681cfa6d69"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5137fd7f-2764-4ce0-adf5-fab8ac4f9924"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a25e7deb-9f32-4d7f-9dce-30a57314f9d6"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b3c8bd4a-50a1-4139-8185-ca38496925ad"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c682e1dc-0ea8-425a-9d49-cd861599082e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cc2c23b4-0b9b-4aa8-ac41-ab049e43aa02"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f02435b3-e8d3-446b-bf4d-59b3678c924e"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItems");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3e231fd5-c961-4c0b-8910-5b681cfa6d69"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5719), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("5137fd7f-2764-4ce0-adf5-fab8ac4f9924"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5711), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("a25e7deb-9f32-4d7f-9dce-30a57314f9d6"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5692), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("b3c8bd4a-50a1-4139-8185-ca38496925ad"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5700), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("c682e1dc-0ea8-425a-9d49-cd861599082e"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5714), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("cc2c23b4-0b9b-4aa8-ac41-ab049e43aa02"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5716), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("f02435b3-e8d3-446b-bf4d-59b3678c924e"), new DateTime(2023, 11, 21, 18, 36, 41, 912, DateTimeKind.Utc).AddTicks(5703), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" }
                });
        }
    }
}
