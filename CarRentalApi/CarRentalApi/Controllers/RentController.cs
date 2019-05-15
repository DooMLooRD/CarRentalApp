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

        [HttpGet]
        public ActionResult<ReservationDetailDTO> GetReservation([FromBody] ReservationDTO reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var foundReservation = _rentService.GetReservation(reservation);
                return Ok(foundReservation);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Reservation not found");
            }
        }
        [HttpPatch]
        public ActionResult<ReservationDetailDTO> UpdateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var updatedReservation = _rentService.UpdateReservation(reservation);
                return Ok(updatedReservation);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Reservation not found");
            }
        }

        [HttpDelete]
        public ActionResult RemoveReservation([FromBody] ReservationDTO reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                _rentService.RemoveReservation(reservation);
                return Ok("Reservation removed");
            }
            catch (ArgumentNullException)
            {
                return NotFound("Reservation not found");
            }
        }

        [HttpPost]
        public ActionResult<ReservationDetailDTO> RentCar([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            return Created("api/Rent", _rentService.RentCar(reservation));
        }

    }
}
