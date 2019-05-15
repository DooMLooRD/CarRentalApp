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
        public DbSet<Reservation> Reservations { get; set; }

        public CarRentalContext(DbContextOptions oprions) : base(oprions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(res =>
            {
                res.OwnsOne(c => c.PickUpLocation);
                res.OwnsOne(c => c.ReturnLocation);
                res.HasKey(c => c.ReservationNumber);
                res.Property(c => c.Surname).IsRequired();
            });
            modelBuilder.Entity<Car>()
                .HasData(
                new[] {
                    new Car
                    {
                        Id=1,
                        ImageURL = "Url",
                        Description = "Simple descr",
                        Price = 10.2
                    },
                    new Car
                    {
                        Id=2,
                        ImageURL = "Url1",
                        Description = "Des",
                        Price = 12.3
                    }
                });
        }
    }
}
