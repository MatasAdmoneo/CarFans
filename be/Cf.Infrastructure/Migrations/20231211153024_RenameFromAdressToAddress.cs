using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFromAdressToAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Services",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Services",
                newName: "Adress");
        }
    }
}
