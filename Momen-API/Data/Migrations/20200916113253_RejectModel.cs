using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RejectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "VendorsUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Vendors",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 16, 13, 32, 53, 385, DateTimeKind.Local).AddTicks(2247),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2020, 9, 4, 13, 45, 20, 813, DateTimeKind.Local).AddTicks(3727));

            migrationBuilder.CreateTable(
                name: "VendorsRejects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 16, 13, 32, 53, 390, DateTimeKind.Local).AddTicks(5454)),
                    Reason = table.Column<string>(nullable: false),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorsRejects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorsRejects_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorsRejects_VendorId",
                table: "VendorsRejects",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorsRejects");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "VendorsUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Vendors",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 4, 13, 45, 20, 813, DateTimeKind.Local).AddTicks(3727),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValue: new DateTime(2020, 9, 16, 13, 32, 53, 385, DateTimeKind.Local).AddTicks(2247));
        }
    }
}
