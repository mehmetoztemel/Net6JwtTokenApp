using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net6JwtTokenApp.Migrations
{
    public partial class RefreshTokenFinishDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenEndDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenEndDate",
                table: "Users");
        }
    }
}
