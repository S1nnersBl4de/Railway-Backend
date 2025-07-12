using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Railwaybackproject.Migrations
{
    /// <inheritdoc />
    public partial class addedjwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "Bookings",
                newName: "BookingTime");

            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingTime",
                table: "Bookings",
                newName: "BookingDate");
        }
    }
}
