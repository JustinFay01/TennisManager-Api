using Microsoft.EntityFrameworkCore.Migrations;
using tennismanager.shared;

#nullable disable

namespace tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Make sure no errors occur if the data already exists
            migrationBuilder.Sql(
                $@"DELETE FROM public.""Users"" where ""Users"".""Id"" = '{SystemUserIds.JustinFayId}'");
            
            migrationBuilder.Sql(
                $@"INSERT INTO public.""Users"" (""Id"", ""FirstName"", ""LastName"", ""UserType"") Values ('{SystemUserIds.JustinFayId}', 'Justin', 'Fay', 'Admin')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $@"DELETE FROM public.""Users"" where ""Users"".""Id"" = '{SystemUserIds.JustinFayId}'");
        }
    }
}
