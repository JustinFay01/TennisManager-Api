using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tennismanager_api.tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultPricePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DefaultPrice",
                table: "Packages",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultPrice",
                table: "Packages");
        }
    }
}
