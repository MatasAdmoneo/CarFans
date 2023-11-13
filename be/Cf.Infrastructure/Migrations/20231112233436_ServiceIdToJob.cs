using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceIdToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceId",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Jobs");
        }
    }
}
