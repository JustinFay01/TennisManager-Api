using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class nameTypeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "SessionIntervals",
                newName: "RecurringStartDate");

            migrationBuilder.AlterColumn<long>(
                name: "RepeatInterval",
                table: "SessionIntervals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecurringStartDate",
                table: "SessionIntervals",
                newName: "StartDate");

            migrationBuilder.AlterColumn<int>(
                name: "RepeatInterval",
                table: "SessionIntervals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
