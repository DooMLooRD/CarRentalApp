using CarRentalApi.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.DTOs
{
    public class ReservationDetailDTO
    {
        public int ReservationNumber { get; set; }

        public Location PickUpLocation { get; set; }
        public Location ReturnLocation { get; set; }

        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Car Car { get; set; }

        public string Surname { get; set; }
        public int Age { get; set; }
        public double TotalPrice { get; set; }
    }
}
                                                                                                                                                                                                                                                                                   