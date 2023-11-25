using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingFormTypeAndOtherBooleanFieldsToAdvertModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Adverts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsLeakedLiquids",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPanelInvalid",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsQuestionsFormType",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsScentBad",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoundBad",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUnstableCar",
                table: "Adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "IsLeakedLiquids",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "IsPanelInvalid",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "IsQuestionsFormType",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "IsScentBad",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "IsSoundBad",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "IsUnstableCar",
                table: "Adverts");
        }
    }
}
