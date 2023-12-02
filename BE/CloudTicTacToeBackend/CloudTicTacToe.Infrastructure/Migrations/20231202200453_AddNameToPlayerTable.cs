using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudTicTacToe.Infrastructure.Migrations
{
    public partial class AddNameToPlayerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");
        }
    }
}
