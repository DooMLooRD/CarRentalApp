import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Reservation, ReservationDetail } from '../models/reservation';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private http: HttpClient) { }

  getAllReservations(): Observable<ReservationDetail[]> {
    return this.http.get<ReservationDetail[]>("https://localhost:44337/api/rent/rentals").pipe(map((res: ReservationDetail[]) => {
      return res;
    }));
  }

  getReservation(reservationNumber: number, surname: string): Observable<ReservationDetail> {
    const httpParams = new HttpParams()
      .set('reservationNumber', reservationNumber.toString())
      .set('surname', surname);

    return this.http.get("https://localhost:44337/api/rent", { params: httpParams }).pipe(map((res: ReservationDetail) => {
      return res;
    }));
  }
  rentCar(reservation: Reservation): Observable<ReservationDetail> {
    reservation.reservationNumber = 0;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post("https://localhost:44337/api/rent", reservation, httpOptions).pipe(map((res: ReservationDetail) => {
      return res;
    }));
  }
  removeReservation(reservationNumber: number, surname: string): Observable<any> {
    const httpParams = new HttpParams()
      .set('reservationNumber', reservationNumber.toString())
      .set('surname', surname);

    return this.http.delete("https://localhost:44337/api/rent", { params: httpParams });
  }

  updateReservation(reservationNumber: number, surname: string, reservation: Reservation): Observable<ReservationDetail> {
    const httpParams = new HttpParams()
      .set('reservationNumber', reservationNumber.toString())
      .set('surname', surname);

    const httpOptions = {
      params: httpParams,
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };
    
    return this.http.put("https://localhost:44337/api/rent", reservation, httpOptions).pipe(map((res: ReservationDetail) => {
      return res;
    }));
  }
}
