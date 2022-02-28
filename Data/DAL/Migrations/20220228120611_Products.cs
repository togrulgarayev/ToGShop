using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DAL.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseProducts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BaseProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "BaseProducts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BaseProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseProducts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BaseProducts");

            migrationBuilder.DropColumn(
                name: "Information",
                table: "BaseProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BaseProducts");
        }
    }
}
