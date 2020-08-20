using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CustomerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorPictures_Vendors_VendorId",
                table: "VendorPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorPictures",
                table: "VendorPictures");

            migrationBuilder.RenameTable(
                name: "VendorPictures",
                newName: "VendorsPictures");

            migrationBuilder.RenameIndex(
                name: "IX_VendorPictures_VendorId",
                table: "VendorsPictures",
                newName: "IX_VendorsPictures_VendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorsPictures",
                table: "VendorsPictures",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    IsRandom = table.Column<bool>(nullable: false, defaultValue: true),
                    IsBlocked = table.Column<bool>(nullable: false, defaultValue: false),
                    ProfilePictureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VendorsPictures_Vendors_VendorId",
                table: "VendorsPictures",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorsPictures_Vendors_VendorId",
                table: "VendorsPictures");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorsPictures",
                table: "VendorsPictures");

            migrationBuilder.RenameTable(
                name: "VendorsPictures",
                newName: "VendorPictures");

            migrationBuilder.RenameIndex(
                name: "IX_VendorsPictures_VendorId",
                table: "VendorPictures",
                newName: "IX_VendorPictures_VendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorPictures",
                table: "VendorPictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorPictures_Vendors_VendorId",
                table: "VendorPictures",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
