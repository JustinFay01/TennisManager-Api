using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class subFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_CoachId",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Sessions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_CoachId",
                table: "Sessions",
                column: "CoachId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_CoachId",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Sessions",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_CoachId",
                table: "Sessions",
                column: "CoachId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
