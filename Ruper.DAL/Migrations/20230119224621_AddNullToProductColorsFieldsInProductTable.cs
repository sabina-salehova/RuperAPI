﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruper.DAL.Migrations
{
    public partial class AddNullToProductColorsFieldsInProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKU",
                table: "ProductColors");
        }
    }
}
