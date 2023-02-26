using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerConsumptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class ComputerInactivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Inactivity",
                table: "PowerConsumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inactivity",
                table: "PowerConsumptions");
        }
    }
}
