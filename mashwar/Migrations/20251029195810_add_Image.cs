using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mashwar.Migrations
{
    /// <inheritdoc />
    public partial class add_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place_Image",
                table: "places",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place_Image",
                table: "places");
        }
    }
}
