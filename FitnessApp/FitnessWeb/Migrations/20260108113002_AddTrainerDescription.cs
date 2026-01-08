using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Trainer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Trainer");
        }
    }
}
