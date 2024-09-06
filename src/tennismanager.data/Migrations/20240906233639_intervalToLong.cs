using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class intervalToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
