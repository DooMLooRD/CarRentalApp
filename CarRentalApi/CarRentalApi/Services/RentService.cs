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

        public List<Car> GetAvailableCars(DateTime pickUpDate, DateTime returnDate)
        {
            var avaibleCars = _context.Cars.Where(car => !_context.Reservations.Any(r => r.CarId == car.Id && r.PickUpDate <= returnDate && r.ReturnDate >= pickUpDate)).ToList();
            return avaibleCars;
        }

        public ReservationDetailDTO RentCar(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return ReservationToDTO(reservation);
        }

        public ReservationDetailDTO GetReservation(ReservationDTO reservation)
        {
            var foundReservation = FindReservation(reservation.ReservationNumber, reservation.Surname);
            if (foundReservation == null)
                throw new ArgumentNullException("Reservation does not exists");
            return ReservationToDTO(foundReservation);
        }

        public ReservationDetailDTO UpdateReservation(Reservation reservation)
        {
            var reservationToUpdate = FindReservation(reservation.ReservationNumber, reservation.Surname);
            if (reservationToUpdate == null)
                throw new ArgumentNullException("Reservation does not exists");
            reservationToUpdate.PickUpDate = reservation.PickUpDate;
            reservationToUpdate.ReturnDate = reservation.ReturnDate;
            reservationToUpdate.PickUpLocation = reservation.PickUpLocation;
            reservationToUpdate.ReturnLocation = reservation.ReturnLocation ?? reservation.PickUpLocation;
            reservationToUpdate.CarId = reservation.CarId;
            _context.Reservations.Update(reservationToUpdate);
            _context.SaveChanges();
            return ReservationToDTO(reservationToUpdate);
        }

        public void RemoveReservation(ReservationDTO reservation)
        {
            var reservationToRemove = FindReservation(reservation.ReservationNumber, reservation.Surname);
            if (reservationToRemove == null)
                throw new ArgumentNullException("Reservation does not exists");
            _context.Reservations.Remove(reservationToRemove);
            _context.SaveChanges();
        }

        private Reservation FindReservation(int reservationNumber, string surname)
        {
            return _context.Reservations.FirstOrDefault(r => r.ReservationNumber == reservationNumber && r.Surname == surname);
        }

        private double CalculateTotalPrice(int age, double price, double totalHours)
        {
            double totalPrice = price * totalHours;
            return Math.Round(age > 25 ? totalPrice * 0.95 : totalPrice, 2);
        }
        private ReservationDetailDTO ReservationToDTO(Reservation reservation)
        {
            _context.Entry(reservation).Reference(c => c.Car).Load();
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
