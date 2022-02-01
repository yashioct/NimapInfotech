using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nimapInfotech.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedByIP = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedByIP = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedByIP = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedByIP = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaster_CategoryMaster_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryMaster",
                columns: new[] { "Id", "CreatedBy", "CreatedByIP", "CreatedOn", "ModifiedBy", "ModifiedByIP", "ModifiedOn", "Name", "isActive" },
                values: new object[] { 1, "Yashi", null, new DateTime(2022, 2, 1, 12, 34, 7, 365, DateTimeKind.Local).AddTicks(7502), null, null, null, "Entertainment", true });

            migrationBuilder.InsertData(
                table: "ProductMaster",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedByIP", "CreatedOn", "ModifiedBy", "ModifiedByIP", "ModifiedOn", "Name", "isActive" },
                values: new object[] { 1, 1, "Yashi", null, new DateTime(2022, 2, 1, 12, 34, 7, 369, DateTimeKind.Local).AddTicks(2308), null, null, null, "Prime Video", true });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_CategoryId",
                table: "ProductMaster",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMaster");

            migrationBuilder.DropTable(
                name: "CategoryMaster");
        }
    }
}
