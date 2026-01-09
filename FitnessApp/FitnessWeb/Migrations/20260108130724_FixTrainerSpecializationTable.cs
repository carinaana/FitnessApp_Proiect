using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessWeb.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainerSpecializationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Trainer");

            migrationBuilder.CreateTable(
                name: "TrainerSpecialization",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerID = table.Column<int>(type: "int", nullable: false),
                    WorkoutTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerSpecialization", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainerSpecialization_Trainer_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "Trainer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerSpecialization_WorkoutType_WorkoutTypeID",
                        column: x => x.WorkoutTypeID,
                        principalTable: "WorkoutType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerSpecialization_TrainerID",
                table: "TrainerSpecialization",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerSpecialization_WorkoutTypeID",
                table: "TrainerSpecialization",
                column: "WorkoutTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerSpecialization");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Trainer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
