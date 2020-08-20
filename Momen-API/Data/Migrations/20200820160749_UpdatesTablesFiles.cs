using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdatesTablesFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "VendorsPictures");

            migrationBuilder.DropColumn(
                name: "LicenseFileName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LogoFileName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PersonalIdFileName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ProfilePictureName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "VendorsPictures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseName",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoName",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalIdName",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfName",
                table: "Contracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "VendorsPictures");

            migrationBuilder.DropColumn(
                name: "LicenseName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LogoName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PersonalIdName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PdfName",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "VendorsPictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseFileName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoFileName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalIdFileName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
