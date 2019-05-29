using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalApi.Migrations
{
    public partial class quickfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageURL = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PickUpLocationId = table.Column<int>(nullable: false),
                    ReturnLocationId = table.Column<int>(nullable: false),
                    PickUpDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationNumber);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_PickUpLocationId",
                        column: x => x.PickUpLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_ReturnLocationId",
                        column: x => x.ReturnLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Description", "ImageURL", "Price" },
                values: new object[,]
                {
                    { 1, "Mercedes Benz with low price - 30.2 dollars per hour! Be quick offers are time limited! Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec accumsan molestie risus vitae porta. Maecenas luctus imperdiet porttitor. Pellentesque vel dapibus mauris. Ut gravida velit non augue viverra, ut luctus odio iaculis. Proin enim ante, congue et viverra vulputate, congue.", "https://images.pexels.com/photos/112460/pexels-photo-112460.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940", 30.199999999999999 },
                    { 2, "Land Rover Range Rover Suv with low price - 42.3 dollars per hour! Be quick offers are time limited! Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut eget risus vel nisl aliquam maximus sed vitae enim. Nam facilisis nec dui sed vulputate. Duis bibendum rutrum nunc in fringilla. Praesent et rhoncus libero. Sed vel interdum purus. Ut enim.", "https://images.pexels.com/photos/116675/pexels-photo-116675.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940", 42.299999999999997 },
                    { 3, "Mercedes-Benz E220d SE Automatic 2.0 with price - 55 dollars per hour! Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia metus a nibh molestie, a egestas metus egestas. Vestibulum ultricies neque suscipit porta tempor. Donec bibendum tempor bibendum. Morbi dapibus porta mattis. Integer dignissim dui at orci tincidunt ultricies eu.", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/2019_Mercedes-Benz_E220d_SE_Automatic_2.0_Front.jpg/800px-2019_Mercedes-Benz_E220d_SE_Automatic_2.0_Front.jpg", 55.0 },
                    { 4, "Mercedes-Benz CLS 350d with price - 60 dollars per hour! Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id dui quis enim tincidunt ultrices. Phasellus vel condimentum quam, at volutpat ipsum. Quisque porttitor purus et justo mattis tempus. Etiam scelerisque ornare nibh, eget sagittis elit tristique non. Praesent pulvinar.", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Mercedes-Benz_CLS_350d_IMG_0901.jpg/800px-Mercedes-Benz_CLS_350d_IMG_0901.jpg", 60.0 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, 51.757928, 19.434822, "Red Car Rental" },
                    { 2, 51.769187000000002, 19.461393000000001, "Blue Car Rental" },
                    { 3, 51.771438000000003, 19.458452999999999, "Yellow Car Rental" },
                    { 4, 51.774113, 19.457691000000001, "Green Car Rental" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PickUpLocationId",
                table: "Reservations",
                column: "PickUpLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReturnLocationId",
                table: "Reservations",
                column: "ReturnLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationNumber_Surname",
                table: "Reservations",
                columns: new[] { "ReservationNumber", "Surname" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
