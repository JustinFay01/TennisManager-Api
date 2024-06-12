using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tennismanager_api.tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class updatedCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachPackagePrices_User_CoachId",
                table: "CoachPackagePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_User_CustomerId",
                table: "CustomerPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSessions_User_CustomerId",
                table: "CustomerSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachPackagePrices_User_CoachId",
                table: "CoachPackagePrices",
                column: "CoachId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_User_CustomerId",
                table: "CustomerPackages",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSessions_User_CustomerId",
                table: "CustomerSessions",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachPackagePrices_User_CoachId",
                table: "CoachPackagePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_User_CustomerId",
                table: "CustomerPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSessions_User_CustomerId",
                table: "CustomerSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachPackagePrices_User_CoachId",
                table: "CoachPackagePrices",
                column: "CoachId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_User_CustomerId",
                table: "CustomerPackages",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSessions_User_CustomerId",
                table: "CustomerSessions",
                column: "CustomerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
