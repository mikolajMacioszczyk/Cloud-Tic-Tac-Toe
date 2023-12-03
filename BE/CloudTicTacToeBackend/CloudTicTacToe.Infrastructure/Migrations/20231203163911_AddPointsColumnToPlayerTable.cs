using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudTicTacToe.Infrastructure.Migrations
{
    public partial class AddPointsColumnToPlayerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_PlayerOId",
                table: "GameBoards");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerOId",
                table: "GameBoards",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_PlayerOId",
                table: "GameBoards",
                column: "PlayerOId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_PlayerOId",
                table: "GameBoards");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerOId",
                table: "GameBoards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_PlayerOId",
                table: "GameBoards",
                column: "PlayerOId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
