using CarRentalApi.Database;
using CarRentalApi.Database.Model;
using CarRentalApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Services
{
    public class RentService
    {
        private CarRentalContext _context;

        public RentService(CarRentalContext context)
        {
            _context = context;
        }
        public async Task<List<Car>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }
        public async Task<List<Car>> GetAvailableCars(DateTime pickUpDate, DateTime returnDate)
        {
            var avaibleCars = await _context.Cars.Where(car => !_context.Reservations.Any(r => r.CarId == car.Id && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate)).ToListAsync();
            return avaibleCars;
        }

        public async Task<ReservationDetailDTO> RentCar(Reservation reservation)
        {
            if (await CheckCarAvailability(reservation.CarId, reservation.PickUpDate, reservation.ReturnDate))
                throw new ArgumentException("Selected Car is not available at this time");
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return await ReservationToDTO(reservation);
        }

        public async Task<ReservationDetailDTO> GetReservation(ReservationIndexDTO reservationIndexDTO)
        {
            var foundReservation = await FindReservation(reservationIndexDTO);
            if (foundReservation == null)
                throw new ArgumentNullException("Reservation does not exists");
            return await ReservationToDTO(foundReservation);
        }
        public async Task<List<ReservationDetailDTO>> GetAllReservation()
        {
            var result = _context.Reservations.Include(c => c.Car).Select(ReservationToDTO);
            await Task.WhenAll(result);
            return result.Select(c => c.Result).ToList();
        }

        public async Task<ReservationDetailDTO> UpdateReservation(ReservationIndexDTO reservationIndexDTO, Reservation reservation)
        {
            if (reservationIndexDTO.ReservationNumber != reservation.ReservationNumber || reservationIndexDTO.Surname != reservation.Surname)
            {
                throw new ArgumentException("Given parameters does not match (ReservationNumber or Surname)");
            }
            var reservationToUpdate = await FindReservation(reservationIndexDTO);
            if (reservationToUpdate == null)
                throw new ArgumentNullException("Reservation does not exists");
            if (await CheckCarAvailability(reservation.CarId, reservation.PickUpDate, reservation.ReturnDate, reservation.ReservationNumber))
                throw new ArgumentException("Selected Car is not available at this time");

            _context.Entry(reservationToUpdate).State = EntityState.Detached;
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await ReservationToDTO(reservation);
        }

        public async Task RemoveReservation(ReservationIndexDTO reservationIndexDTO)
        {
            var reservationToRemove = await FindReservation(reservationIndexDTO);
            if (reservationToRemove == null)
                throw new ArgumentNullException("Reservation does not exists");
            _context.Reservations.Remove(reservationToRemove);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckCarAvailability(int carId, DateTime pickUpDate, DateTime returnDate, int? exceptReservation = null)
        {
            return !await _context.Reservations.AnyAsync(r => r.CarId == carId && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate && exceptReservation != null ? exceptReservation.Value != r.ReservationNumber : true);
        }

        private async Task<Reservation> FindReservation(ReservationIndexDTO reservationIndexDTO)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationNumber == reservationIndexDTO.ReservationNumber && r.Surname == reservationIndexDTO.Surname);
        }

        private double CalculateTotalPrice(int age, double price, double totalHours)
        {
            double totalPrice = price * totalHours;
            return Math.Round(age > 25 ? totalPrice * 0.95 : totalPrice, 2);
        }
        private async Task<ReservationDetailDTO> ReservationToDTO(Reservation reservation)
        {
            await _context.Entry(reservation).Reference(c => c.Car).LoadAsync();
            return new ReservationDetailDTO
            {
                ReservationNumber = reservation.ReservationNumber,
                PickUpDate = reservation.PickUpDate,
                ReturnDate = reservation.ReturnDate,
                PickUpLocation = reservation.PickUpLocation,
                ReturnLocation = reservation.ReturnLocation,
                Car = reservation.Car,
                Surname = reservation.Surname,
                Age = reservation.Age,
                TotalPrice = CalculateTotalPrice(reservation.Age, reservation.Car.Price, (reservation.ReturnDate - reservation.PickUpDate).TotalHours)
            };
        }
    }
}
