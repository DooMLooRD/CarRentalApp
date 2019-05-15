using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalApi.Migrations
{
    public partial class addSurnameAndReservationNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationNumber_Surname",
                table: "Reservations",
                columns: new[] { "ReservationNumber", "Surname" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationNumber_Surname",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
