using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudTicTacToe.Infrastructure.Migrations
{
    public partial class AddedNextPLayerIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NextPlayerId",
                table: "GameBoards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextPlayerId",
                table: "GameBoards");
        }
    }
}
