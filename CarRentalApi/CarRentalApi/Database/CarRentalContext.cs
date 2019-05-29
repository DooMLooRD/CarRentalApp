using CarRentalApi.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Database
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public CarRentalContext(DbContextOptions oprions) : base(oprions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(res =>
            {
                res.HasKey(c => c.ReservationNumber);
                res.Property(c => c.Surname).IsRequired();
                res.HasOne(c => c.PickUpLocation).WithMany().HasForeignKey(c=>c.PickUpLocationId).OnDelete(DeleteBehavior.Restrict);
                res.HasOne(c => c.ReturnLocation).WithMany().HasForeignKey(c=>c.ReturnLocationId).OnDelete(DeleteBehavior.Restrict);
                res.HasIndex(c => new { c.ReservationNumber, c.Surname }).IsUnique();
            });
            modelBuilder.Entity<Location>()
                .HasData(new[]{
                    new Location(){ Id=1, Name="Red Car Rental", Latitude=51.757928, Longitude=19.434822},
                    new Location(){ Id=2, Name="Blue Car Rental", Latitude=51.769187, Longitude=19.461393},
                    new Location(){ Id=3, Name="Yellow Car Rental", Latitude=51.771438, Longitude=19.458453},
                    new Location(){ Id=4, Name="Green Car Rental", Latitude=51.774113, Longitude=19.457691},
                    });
            modelBuilder.Entity<Car>()
                .HasData(
                new[] {
                    new Car
                    {
                        Id=1,
                        ImageURL = "https://images.pexels.com/photos/112460/pexels-photo-112460.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                        Description = "Mercedes Benz with low price - 30.2 dollars per hour! Be quick offers are time limited! " +
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec accumsan molestie risus vitae porta. " +
                        "Maecenas luctus imperdiet porttitor. Pellentesque vel dapibus mauris. Ut gravida velit non augue viverra, ut luctus odio iaculis. " +
                        "Proin enim ante, congue et viverra vulputate, congue.",
                        Price = 30.2
                    },
                    new Car
                    {
                        Id=2,
                        ImageURL = "https://images.pexels.com/photos/116675/pexels-photo-116675.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                        Description = "Land Rover Range Rover Suv with low price - 42.3 dollars per hour! Be quick offers are time limited! " +
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut eget risus vel nisl aliquam maximus sed vitae enim. " +
                        "Nam facilisis nec dui sed vulputate. Duis bibendum rutrum nunc in fringilla. Praesent et rhoncus libero. " +
                        "Sed vel interdum purus. Ut enim.",
                        Price = 42.3
                    },
                    new Car
                    {
                        Id=3,
                        ImageURL="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/2019_Mercedes-Benz_E220d_SE_Automatic_2.0_Front.jpg/800px-2019_Mercedes-Benz_E220d_SE_Automatic_2.0_Front.jpg",
                        Description="Mercedes-Benz E220d SE Automatic 2.0 with price - 55 dollars per hour! Lorem ipsum dolor sit amet, " +
                        "consectetur adipiscing elit. Suspendisse lacinia metus a nibh molestie, a egestas metus egestas. " +
                        "Vestibulum ultricies neque suscipit porta tempor. Donec bibendum tempor bibendum. Morbi dapibus porta mattis. " +
                        "Integer dignissim dui at orci tincidunt ultricies eu.",
                        Price = 55
                    },
                    new Car
                    {
                        Id=4,
                        ImageURL="https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Mercedes-Benz_CLS_350d_IMG_0901.jpg/800px-Mercedes-Benz_CLS_350d_IMG_0901.jpg",
                        Description="Mercedes-Benz CLS 350d with price - 60 dollars per hour! " +
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id dui quis enim tincidunt ultrices. " +
                        "Phasellus vel condimentum quam, at volutpat ipsum. Quisque porttitor purus et justo mattis tempus. " +
                        "Etiam scelerisque ornare nibh, eget sagittis elit tristique non. Praesent pulvinar.",
                        Price=60
                    }
                });
        }
    }
}

