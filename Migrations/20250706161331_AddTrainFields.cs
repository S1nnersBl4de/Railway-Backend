using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railwaybackproject.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrainName",
                table: "Trains",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Trains",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Trains",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Trains");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trains",
                newName: "TrainName");
        }
    }
}
