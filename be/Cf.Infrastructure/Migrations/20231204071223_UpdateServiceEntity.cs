using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<byte[]>>(
                name: "Data",
                table: "Services",
                type: "bytea[]",
                nullable: true,
                oldClrType: typeof(List<byte[]>),
                oldType: "bytea[]");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Services",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Services");

            migrationBuilder.AlterColumn<List<byte[]>>(
                name: "Data",
                table: "Services",
                type: "bytea[]",
                nullable: false,
                oldClrType: typeof(List<byte[]>),
                oldType: "bytea[]",
                oldNullable: true);
        }
    }
}
