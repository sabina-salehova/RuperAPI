using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ruper.DAL.Migrations
{
    public partial class AddIsDeletedFieldIEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Sliders",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Sliders",
                newName: "IsActive");
        }
    }
}
