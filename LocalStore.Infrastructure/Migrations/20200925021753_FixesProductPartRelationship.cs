using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalStore.Infrastructure.Migrations
{
    public partial class FixesProductPartRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductParts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductParts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
