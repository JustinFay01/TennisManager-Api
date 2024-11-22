using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class nuked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
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
                name: "Groups",
                columns: table => new
                {
                    MemberNumber = table.Column<int>(type: "integer", maxLength: 5, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.MemberNumber);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Nickname = table.Column<string>(type: "text", nullable: true),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    UserType = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Uses = table.Column<int>(type: "integer", nullable: false),
                    CoachId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Packages_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    CoachId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    MemberNumber = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupMemberNumber = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.MemberNumber, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupMemberNumber",
                        column: x => x.GroupMemberNumber,
                        principalTable: "Groups",
                        principalColumn: "MemberNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSessions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSessions_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSessions_CustomerId",
                table: "CustomerSessions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSessions_SessionId",
                table: "CustomerSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ParentEventId",
                table: "Event",
                column: "ParentEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CoachId",
                table: "Packages",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CustomerId",
                table: "Packages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPattern_EventId",
                table: "RecurringPattern",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CoachId",
                table: "Sessions",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_EventId",
                table: "Sessions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupMemberNumber",
                table: "UserGroups",
                column: "GroupMemberNumber");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerSessions");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "RecurringPattern");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
