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
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            return Ok(await _rentService.GetAllCars());
        }

        [HttpGet]
        [Route("availableCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAvaibleCars([FromBody]DateFromToDTO dateFromTo)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            return Ok(await _rentService.GetAvailableCars(dateFromTo.PickUpDate, dateFromTo.ReturnDate));
        }

        [HttpGet]
        [Route("rentals")]
        public async Task<ActionResult<IEnumerable<ReservationDetailDTO>>> GetAllRentals()
        {
            return Ok(await _rentService.GetAllReservation());
        }

        [HttpGet]
        public async Task<ActionResult<ReservationDetailDTO>> GetReservation([FromQuery] ReservationIndexDTO reservationIndexDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var foundReservation = await _rentService.GetReservation(reservationIndexDTO);
                return Ok(foundReservation);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Reservation not found");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ReservationDetailDTO>> UpdateReservation([FromQuery] ReservationIndexDTO reservationIndexDTO, [FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var updatedReservation = await _rentService.UpdateReservation(reservationIndexDTO, reservation);
                return Ok(updatedReservation);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Reservation not found");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveReservation([FromQuery] ReservationIndexDTO reservationIndexDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                await _rentService.RemoveReservation(reservationIndexDTO);
                return Ok("Reservation removed");
            }
            catch (ArgumentNullException)
            {
                return NotFound("Reservation not found");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDetailDTO>> RentCar([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var result = await _rentService.RentCar(reservation);
            return CreatedAtAction(nameof(GetReservation), new ReservationIndexDTO { ReservationNumber = result.ReservationNumber, Surname = result.Surname }, result);
        }

    }
}
