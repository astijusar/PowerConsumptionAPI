using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerConsumptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class RequirePcIdInPowerConsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerConsumptions_Computers_ComputerId",
                table: "PowerConsumptions");

            migrationBuilder.UpdateData(
                table: "PowerConsumptions",
                keyColumn: "ComputerId",
                keyValue: null,
                column: "ComputerId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ComputerId",
                table: "PowerConsumptions",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerConsumptions_Computers_ComputerId",
                table: "PowerConsumptions",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerConsumptions_Computers_ComputerId",
                table: "PowerConsumptions");

            migrationBuilder.AlterColumn<string>(
                name: "ComputerId",
                table: "PowerConsumptions",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerConsumptions_Computers_ComputerId",
                table: "PowerConsumptions",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id");
        }
    }
}
