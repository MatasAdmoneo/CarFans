using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldNAme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Reviews",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Reviews",
                newName: "Score");
        }
    }
}
