using CarRentalApi.Database;
using CarRentalApi.Database.Model;
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

        public List<Car> GetAvailableCars(DateTime pickUpDate, DateTime returnDate)
        {
            var avaibleCars = _context.Cars.Where(car => !_context.Reservations.Any(r => r.CarId == car.Id && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate)).ToList();
            return avaibleCars;
        }

        public int RentCar(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation.ReservationNumber;
        }

        public Reservation GetReservation(int reservationNumber, string surname)
        {
            return _context.Reservations.First(r => r.ReservationNumber == reservationNumber && r.Surname == surname);
        }
        public Reservation UpdateReservation(Reservation reservation)
        {
            var reservationToUpdate = GetReservation(reservation.ReservationNumber, reservation.Surname);
            reservationToUpdate.PickUpDate = reservation.PickUpDate;
            reservationToUpdate.ReturnDate = reservation.ReturnDate;
            reservationToUpdate.PickUpLocation = reservation.PickUpLocation;
            reservationToUpdate.ReturnLocation = reservation.ReturnLocation;
            _context.Reservations.Update(reservationToUpdate);
            _context.SaveChanges();
            return reservationToUpdate;
        }
        public void RemoveReservation(int reservationNumber, string surname)
        {
            _context.Reservations.Remove(GetReservation(reservationNumber, surname));
            _context.SaveChanges();
        }
    }
}
