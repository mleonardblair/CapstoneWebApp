using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class UserModel2ndUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
