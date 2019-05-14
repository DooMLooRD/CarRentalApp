using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Database.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
