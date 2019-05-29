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
            return await _context.Cars.AsNoTracking().ToListAsync();
        }
        public async Task<List<Car>> GetAvailableCars(DateTime pickUpDate, DateTime returnDate, int? reservationNumber = null)
        {
            List<Car> avaibleCars;
            if (reservationNumber == null)
                avaibleCars = await _context.Cars.AsNoTracking().Where(car => !_context.Reservations.AsNoTracking().Any(r => r.CarId == car.Id && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate)).ToListAsync();
            else
                avaibleCars = await _context.Cars.AsNoTracking().Where(car => !_context.Reservations.Where(r => r.ReservationNumber != reservationNumber.Value).AsNoTracking().Any(r => r.CarId == car.Id && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate)).ToListAsync();
            return avaibleCars;
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _context.Locations.AsNoTracking().ToListAsync();
        }

        public async Task<ReservationDetailDTO> RentCar(Reservation reservation)
        {
            if (!await CheckCarAvailability(reservation.CarId, reservation.PickUpDate, reservation.ReturnDate))
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
            List<ReservationDetailDTO> result = new List<ReservationDetailDTO>();
            var reservations = _context.Reservations;
            foreach (var reservation in reservations)
            {
                result.Add(await ReservationToDTO(reservation));
            }
            return result;
        }

        public async Task<ReservationDetailDTO> UpdateReservation(ReservationIndexDTO reservationIndexDTO, Reservation reservation)
        {
            if (reservationIndexDTO.ReservationNumber != reservation.ReservationNumber || reservationIndexDTO.Surname != reservation.Surname)
            {
                throw new ArgumentException("Given parameters does not match (ReservationNumber or Surname)");
            }
            if (await FindReservation(reservationIndexDTO, true) == null)
                throw new ArgumentNullException("Reservation does not exists");
            if (!await CheckCarAvailability(reservation.CarId, reservation.PickUpDate, reservation.ReturnDate, reservation.ReservationNumber))
                throw new ArgumentException("Selected Car is not available at this time");
            _context.Update(reservation);
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
            if (await _context.Reservations.CountAsync() == 0)
                return true;
            if (exceptReservation == null)
                return !await _context.Reservations.AsNoTracking().AnyAsync(r => r.CarId == carId && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate);
            return !await _context.Reservations.Where(r => exceptReservation.Value != r.ReservationNumber).AsNoTracking().AnyAsync(r => r.CarId == carId && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate);
        }

        private async Task<Reservation> FindReservation(ReservationIndexDTO reservationIndexDTO, bool asNoTracking = false)
        {
            if (asNoTracking)
            {
                return await _context.Reservations.AsNoTracking().FirstOrDefaultAsync(r => r.ReservationNumber == reservationIndexDTO.ReservationNumber && r.Surname == reservationIndexDTO.Surname);
            }
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
            await _context.Entry(reservation).Reference(c => c.PickUpLocation).LoadAsync();
            await _context.Entry(reservation).Reference(c => c.ReturnLocation).LoadAsync();
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
