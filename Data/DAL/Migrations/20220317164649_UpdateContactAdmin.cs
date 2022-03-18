using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.DAL.Migrations
{
    public partial class UpdateContactAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ContactAdmin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ContactAdmin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ContactAdmin");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ContactAdmin");
        }
    }
}
