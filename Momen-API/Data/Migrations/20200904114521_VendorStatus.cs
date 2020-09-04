using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class VendorStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Vendors",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 4, 13, 45, 20, 813, DateTimeKind.Local).AddTicks(3727));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Vendors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ContractId",
                table: "Vendors",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Contracts_ContractId",
                table: "Vendors",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Contracts_ContractId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_ContractId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendors");
        }
    }
}
