import { Location } from './location';
import { Car } from './car';

export class Reservation {
  reservationNumber: number;
  pickUpLocationId: number;
  returnLocationId: number;
  pickUpDate: Date;
  returnDate: Date;
  carId: number;
  surname: string;
  age: number;
}
export function InitReservation() : Reservation{
  return {
    reservationNumber : 0,
    pickUpLocationId : 0,
    returnLocationId : 0,
    pickUpDate : new Date(),
    returnDate: new  Date(),
    carId: 0,
    surname: '',
    age: 0
  };
}
export class ReservationDetail{
  reservationNumber: number;
  pickUpLocation: Location;
  returnLocation: Location;
  pickUpDate: Date;
  returnDate: Date;
  car: Car;
  surname: string;
  age: number;
  totalPrice: number;
}

export function detailToReservation(detail: ReservationDetail) : Reservation{
  return {
    reservationNumber: detail.reservationNumber,
    pickUpLocationId: detail.pickUpLocation.id,
    returnLocationId: detail.returnLocation.id,
    pickUpDate: detail.pickUpDate,
    returnDate: detail.returnDate,
    carId: detail.car.id,
    surname: detail.surname,
    age: detail.age
  };
}