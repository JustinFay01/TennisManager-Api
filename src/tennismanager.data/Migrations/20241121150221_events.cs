using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionMetas_SessionId",
                table: "SessionMetas");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Sessions");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Sessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "SessionMetas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsFullDay = table.Column<bool>(type: "boolean", nullable: false),
                    IsRecurring = table.Column<bool>(type: "boolean", nullable: false),
                    ParentEventId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Event_ParentEventId",
                        column: x => x.ParentEventId,
                        principalTable: "Event",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecurringPattern",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecurringType = table.Column<int>(type: "integer", nullable: false),
                    SeparationCount = table.Column<int>(type: "integer", nullable: false),
                    MaxOccurrences = table.Column<int>(type: "integer", nullable: true),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: true),
                    WeekOfMonth = table.Column<int>(type: "integer", nullable: true),
                    DayOfMonth = table.Column<int>(type: "integer", nullable: true),
                    MonthOfYear = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPattern", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringPattern_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_EventId",
                table: "Sessions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionMetas_SessionId",
                table: "SessionMetas",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ParentEventId",
                table: "Event",
                column: "ParentEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPattern_EventId",
                table: "RecurringPattern",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Event_EventId",
                table: "Sessions",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Event_EventId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "RecurringPattern");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_EventId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionMetas_SessionId",
                table: "SessionMetas");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "SessionMetas");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SessionMetas_SessionId",
                table: "SessionMetas",
                column: "SessionId",
                unique: true);
        }
    }
}
