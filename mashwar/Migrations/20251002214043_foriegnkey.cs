using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mashwar.Migrations
{
    /// <inheritdoc />
    public partial class Foriegnkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Image",
            //    table: "places");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_places_User_Id",
                table: "places",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_places_Users_User_Id",
                table: "places",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_places_Users_User_Id",
                table: "places");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_places_User_Id",
                table: "places");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "places");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
