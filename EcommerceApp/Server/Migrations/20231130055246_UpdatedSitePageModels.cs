using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Server.Migrations
{
    public partial class UpdatedSitePageModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paragraph = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageAlt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BannerImageURI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQAnswer_FAQs_FAQId",
                        column: x => x.FAQId,
                        principalTable: "FAQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAQQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQQuestion_FAQs_FAQId",
                        column: x => x.FAQId,
                        principalTable: "FAQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryImage_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1e9b3e30-8dc3-4de3-a2f4-d0633cbd1754"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2127), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The art of beautiful handwriting, often used in invitations, letters, and decorative texts.", "Calligraphy" },
                    { new Guid("7a9bdf3d-5bfc-40be-aa30-b9218eccb1e0"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2130), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items that serve a decorative purpose, enhancing the aesthetics of a space without serving a practical function.", "Decorative" },
                    { new Guid("8d6adaf6-3f09-4c96-9e56-1222c23b2df0"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2112), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor and indoor ornaments that add flair to living spaces and gardens.", "Ornament" },
                    { new Guid("aea7805c-a3ba-48a3-b2f4-663493731e39"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2121), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Circular arrangements of flowers, leaves, or other materials, often used for decorative purposes or special occasions.", "Wreaths" },
                    { new Guid("d22f7215-96f6-4fe2-aee2-4acb951fe23e"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2119), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Items related to garden decorations and maintenance, including plants, tools, and accessories.", "Garden" },
                    { new Guid("d368a589-8538-4eed-b22e-678f4fcccdd6"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2123), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A form of textile produced using knotting techniques, often used for wall hangings, plant hangers, and more.", "Macrame" },
                    { new Guid("f454fa0e-79e7-494e-b742-118c82b15433"), new DateTime(2023, 11, 30, 5, 52, 46, 390, DateTimeKind.Utc).AddTicks(2128), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The craft of decorating fabric using a needle to apply thread or yarn, often used in apparel, home decor, and artworks.", "Embroidery" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQAnswer_FAQId",
                table: "FAQAnswer",
                column: "FAQId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQQuestion_FAQId",
                table: "FAQQuestion",
                column: "FAQId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImage_GalleryId",
                table: "GalleryImage",
                column: "GalleryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "FAQAnswer");

            migrationBuilder.DropTable(
                name: "FAQQuestion");

            migrationBuilder.DropTable(
                name: "GalleryImage");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1e9b3e30-8dc3-4de3-a2f4-d0633cbd1754"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7a9bdf3d-5bfc-40be-aa30-b9218eccb1e0"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8d6adaf6-3f09-4c96-9e56-1222c23b2df0"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("aea7805c-a3ba-48a3-b2f4-663493731e39"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d22f7215-96f6-4fe2-aee2-4acb951fe23e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d368a589-8538-4eed-b22e-678f4fcccdd6"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f454fa0e-79e7-494e-b742-118c82b15433"));

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
        }
    }
}
