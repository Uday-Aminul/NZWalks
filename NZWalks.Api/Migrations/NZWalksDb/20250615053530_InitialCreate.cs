using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations.NZWalksDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LengthInKm = table.Column<double>(type: "float", nullable: false),
                    WalkImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walks_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Easy" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Medium" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("12121212-1212-1212-1212-121212121212"), "TAS", "Taranaki", null },
                    { new Guid("34343434-3434-3434-3434-343434343434"), "HKB", "Hawke's Bay", null },
                    { new Guid("56565656-5656-5656-5656-565656565656"), "MBH", "Marlborough", null },
                    { new Guid("78787878-7878-7878-7878-787878787878"), "OTG", "Otago", null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "AKL", "Auckland", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "WGN", "Wellington", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "CAN", "Canterbury", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "NSN", "Nelson", null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "BOP", "Bay of Plenty", null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "WKO", "Waikato", null }
                });

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Description", "DifficultyId", "LengthInKm", "Name", "RegionId", "WalkImageUrl" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000001"), "Spectacular volcanic walk", new Guid("33333333-3333-3333-3333-333333333333"), 19.399999999999999, "Tongariro Alpine Crossing", new Guid("12121212-1212-1212-1212-121212121212"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000002"), "Coastal track with beaches", new Guid("22222222-2222-2222-2222-222222222222"), 60.0, "Abel Tasman Coast Track", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000003"), "Mountainous scenery", new Guid("33333333-3333-3333-3333-333333333333"), 32.0, "Routeburn Track", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000004"), "Coastal forest walk", new Guid("22222222-2222-2222-2222-222222222222"), 70.0, "Queen Charlotte Track", new Guid("56565656-5656-5656-5656-565656565656"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000005"), "Great walk with lakes and forests", new Guid("33333333-3333-3333-3333-333333333333"), 60.0, "Kepler Track", new Guid("78787878-7878-7878-7878-787878787878"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000006"), "Diverse landscapes", new Guid("22222222-2222-2222-2222-222222222222"), 78.0, "Heaphy Track", new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000007"), "Short easy nature walk", new Guid("11111111-1111-1111-1111-111111111111"), 2.0, "Mount Bruce Walk", new Guid("34343434-3434-3434-3434-343434343434"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000008"), "Rainforest walk near Auckland", new Guid("22222222-2222-2222-2222-222222222222"), 10.0, "Waitakere Ranges Track", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000009"), "Lakeside walk", new Guid("33333333-3333-3333-3333-333333333333"), 46.0, "Lake Waikaremoana Great Walk", new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000010"), "Coastal and forest walk", new Guid("22222222-2222-2222-2222-222222222222"), 55.0, "Paparoa Track", new Guid("78787878-7878-7878-7878-787878787878"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000011"), "Stewart Island walk", new Guid("22222222-2222-2222-2222-222222222222"), 32.0, "Rakiura Track", new Guid("78787878-7878-7878-7878-787878787878"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000012"), "Scenic coastal walk", new Guid("11111111-1111-1111-1111-111111111111"), 20.0, "Coromandel Coastal Walk", new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000013"), "Volcanic alpine circuit", new Guid("33333333-3333-3333-3333-333333333333"), 35.0, "Pouakai Circuit", new Guid("12121212-1212-1212-1212-121212121212"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000014"), "Hilltop views", new Guid("11111111-1111-1111-1111-111111111111"), 5.0, "Queenstown Hill Walk", new Guid("78787878-7878-7878-7878-787878787878"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000015"), "Long-distance trail", new Guid("33333333-3333-3333-3333-333333333333"), 3000.0, "Te Araroa Trail", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
