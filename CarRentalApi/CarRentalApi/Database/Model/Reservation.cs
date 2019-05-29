using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Database.Model
{
    public class Reservation
    {
        public int ReservationNumber { get; set; }


        public int PickUpLocationId { get; set; }
        public Location PickUpLocation { get; set; }

        public int ReturnLocationId { get; set; }
        public Location ReturnLocation { get; set; }

        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public string Surname { get; set; }
        public int Age { get; set; }

    }
}
