using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerConsumptionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPowerConsumptionToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02b17ea3-62ea-421d-abba-d67072d1a523");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32fe274f-fbd0-46bb-bea9-0568a3a7b8ca");

            migrationBuilder.AddColumn<float>(
                name: "TotalPowerDraw",
                table: "PowerConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e421773b-4247-4133-af99-a19fcc180c69", "4aade683-4611-4909-b50e-b82822b637e1", "User", "USER" },
                    { "e90a6274-df4b-4511-a71d-d4f98c0a46db", "22177730-4c13-4baa-a7c9-eda32ded1f72", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e421773b-4247-4133-af99-a19fcc180c69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e90a6274-df4b-4511-a71d-d4f98c0a46db");

            migrationBuilder.DropColumn(
                name: "TotalPowerDraw",
                table: "PowerConsumptions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02b17ea3-62ea-421d-abba-d67072d1a523", "0f9abb76-f1a9-4dfb-8c03-526b37d8b46c", "User", "USER" },
                    { "32fe274f-fbd0-46bb-bea9-0568a3a7b8ca", "822586b1-0fc2-4f99-bedb-0aba9b0c1b18", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
