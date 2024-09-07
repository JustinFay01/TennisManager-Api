using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class overHaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    CoachId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "SessionMetas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Recurring = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionMetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionMetas_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionIntervals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionMetaId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecurringStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RepeatInterval = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionIntervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionIntervals_SessionMetas_SessionMetaId",
                        column: x => x.SessionMetaId,
                        principalTable: "SessionMetas",
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
                name: "IX_Packages_CoachId",
                table: "Packages",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CustomerId",
                table: "Packages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionIntervals_SessionMetaId",
                table: "SessionIntervals",
                column: "SessionMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionMetas_SessionId",
                table: "SessionMetas",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CoachId",
                table: "Sessions",
                column: "CoachId");

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
                name: "SessionIntervals");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "SessionMetas");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
