using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Database.Model;
using CarRentalApi.DTOs;
using CarRentalApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private RentService _rentService;

        public RentController(RentService rentService)
        {
            _rentService = rentService;
        }

        // GET: api/Rent
        [HttpGet]
        [Route("cars")]
        public ActionResult<IEnumerable<Car>> GetAvaibleCars([FromBody]DateFromToDTO dateFromTo)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            return Ok(_rentService.GetAvailableCars(dateFromTo.PickUpDate, dateFromTo.ReturnDate));
        }

        // GET: api/Rent/5
        [HttpGet]
        public ActionResult<Reservation> GetReservation([FromBody] ReservationIndexDTO reservationIndex)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                return Ok(_rentService.GetReservation(reservationIndex.ReservationNumber, reservationIndex.Surname));
            }
            catch (InvalidOperationException)
            {
                return NotFound("Reservation not found");
            }
        }

        [HttpDelete]
        public ActionResult<Reservation> RemoveReservation([FromBody] ReservationIndexDTO reservationIndex)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                _rentService.RemoveReservation(reservationIndex.ReservationNumber, reservationIndex.Surname);
                return Ok("Reservation removed");
            }
            catch (InvalidOperationException)
            {
                return NotFound("Reservation not found");
            }
        }

        // POST: api/Rent
        [HttpPost]
        public ActionResult<int> RentCar([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            return Created("api/Rent", _rentService.RentCar(reservation));
        }

    }
}
