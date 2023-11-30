using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class SitePageModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("207e27fc-9d92-4d3f-8fde-1a352b0bfcac"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5411), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("5a2831b7-4420-45dd-ae6d-753ffc8d716d"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5413), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("71f53b7e-ac03-4d4a-8694-0af17a8d5444"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5409), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("7e099854-32e8-42fe-bd2e-d72d952c9fb5"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5426), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" },
                    { new Guid("8da47b2b-d592-465d-ab0c-5ba6142e0380"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5425), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("90e35faf-0b34-4b82-87f1-f27ddd2b5406"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5404), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("acb53e2f-c695-4ac9-bf69-25985d45d5f2"), new DateTime(2023, 11, 30, 4, 5, 22, 872, DateTimeKind.Utc).AddTicks(5427), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("207e27fc-9d92-4d3f-8fde-1a352b0bfcac"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5a2831b7-4420-45dd-ae6d-753ffc8d716d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("71f53b7e-ac03-4d4a-8694-0af17a8d5444"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7e099854-32e8-42fe-bd2e-d72d952c9fb5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8da47b2b-d592-465d-ab0c-5ba6142e0380"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("90e35faf-0b34-4b82-87f1-f27ddd2b5406"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("acb53e2f-c695-4ac9-bf69-25985d45d5f2"));

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
    }
}
