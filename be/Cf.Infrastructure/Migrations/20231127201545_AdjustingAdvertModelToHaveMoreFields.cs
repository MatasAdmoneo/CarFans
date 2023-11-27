using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingAdvertModelToHaveMoreFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Adverts",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "ManufactureYear",
                table: "Adverts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Adverts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Adverts");

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

            migrationBuilder.DropColumn(
                name: "ManufactureYear",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Adverts");
        }
    }
}
