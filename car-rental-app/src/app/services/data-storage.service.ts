import { Injectable } from '@angular/core';
import { ReservationDetail } from '../models/reservation';

@Injectable({
  providedIn: 'root'
})
export class DataStorageService {
  private reservationSummary: ReservationDetail;

  constructor() { }
  setReservationSummary(reservationSummary: ReservationDetail) {
    this.reservationSummary = reservationSummary;
  }
  getReservationSummary(): ReservationDetail {
    return this.reservationSummary;
  }
}
