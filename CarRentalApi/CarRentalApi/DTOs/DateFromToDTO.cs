using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.DTOs
{
    public class DateFromToDTO
    {
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
