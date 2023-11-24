using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class CartItemRemoveQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3bdb9898-fae4-4b4e-90a3-cd9941045a39"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4789b2a7-e421-4135-9c9f-4c909837b1ec"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6373cd30-b646-4a50-92ea-23c2673696ff"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a420ef4b-48e6-41be-bcbb-9e421f889d23"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b42c8862-3cb1-4c66-a6cb-80a901d706c4"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b5504260-4a82-49f7-ac6d-fa33db5fe6ec"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b85a2208-2238-4a0f-bc5e-06beb68ccc91"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                table: "ApplicationUsers",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts");

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

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "Quantity",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3bdb9898-fae4-4b4e-90a3-cd9941045a39"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(701), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("4789b2a7-e421-4135-9c9f-4c909837b1ec"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(710), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("6373cd30-b646-4a50-92ea-23c2673696ff"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(708), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("a420ef4b-48e6-41be-bcbb-9e421f889d23"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(660), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("b42c8862-3cb1-4c66-a6cb-80a901d706c4"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(676), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("b5504260-4a82-49f7-ac6d-fa33db5fe6ec"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(704), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("b85a2208-2238-4a0f-bc5e-06beb68ccc91"), new DateTime(2023, 11, 15, 2, 38, 14, 966, DateTimeKind.Utc).AddTicks(679), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId");
        }
    }
}
