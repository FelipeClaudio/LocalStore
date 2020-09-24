using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalStore.Infrastructure.Migrations
{
    public partial class DefaultDateTimeToProductPartMaterialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "ProductPartMaterials",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "ProductPartMaterials",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");
        }
    }
}
