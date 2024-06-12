using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualBasic;
using tennismanager_api.tennismanager.constants;

#nullable disable

namespace tennismanager_api.tennismanager.data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Make sure no errors occur if the data already exists
            migrationBuilder.Sql(
                $@"DELETE FROM public.""User"" where ""User"".""Id"" = '{SystemUserIds.JustinFayId}'");
            
            migrationBuilder.Sql(
                $@"INSERT INTO public.""User"" (""Id"", ""FirstName"", ""LastName"", ""UserType"") Values ('{SystemUserIds.JustinFayId}', 'Justin', 'Fay', 'Coach')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $@"DELETE FROM public.""User"" where ""User"".""Id"" = '{SystemUserIds.JustinFayId}'");
        }
    }
}
