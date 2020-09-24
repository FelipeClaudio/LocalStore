using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalStore.Infrastructure.Migrations
{
    public partial class AddedProductPartMaterialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParts_Materials_MaterialId",
                table: "ProductParts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts");

            migrationBuilder.DropIndex(
                name: "IX_ProductParts_MaterialId",
                table: "ProductParts");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "ProductParts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductParts",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "ProductParts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductPartMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ProductPartId = table.Column<Guid>(nullable: false),
                    MaterialId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPartMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPartMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPartMaterials_ProductParts_ProductPartId",
                        column: x => x.ProductPartId,
                        principalTable: "ProductParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPartMaterials_MaterialId",
                table: "ProductPartMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPartMaterials_ProductPartId",
                table: "ProductPartMaterials",
                column: "ProductPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts");

            migrationBuilder.DropTable(
                name: "ProductPartMaterials");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "ProductParts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductParts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "ProductParts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductParts_MaterialId",
                table: "ProductParts",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParts_Materials_MaterialId",
                table: "ProductParts",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParts_Products_ProductId",
                table: "ProductParts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
