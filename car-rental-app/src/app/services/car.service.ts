import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  getAllCars(): Observable<Car[]> {
    return this.http.get("https://localhost:44337/api/rent/cars").pipe(map((res: Car[]) => {
      return res;
    }));
  }

  getAvailableCars(pickUpDate: Date, returnDate: Date): Observable<Car[]> {
    const httpParams = new HttpParams()
      .set('pickUpDate', new Date(pickUpDate).toUTCString())
      .set('returnDate', new Date(returnDate).toUTCString());
      
    return this.http.get("https://localhost:44337/api/rent/availableCars", { params: httpParams }).pipe(map((res: Car[]) => {
      return res;
    }));
  }
  getAvailableCarsForReservation(pickUpDate: Date, returnDate: Date, reservationNumber: number): Observable<Car[]> {
    const httpParams = new HttpParams()
      .set('pickUpDate', new Date(pickUpDate).toUTCString())
      .set('returnDate', new Date(returnDate).toUTCString());
      
    return this.http.get("https://localhost:44337/api/rent/availableCars/"+reservationNumber, { params: httpParams }).pipe(map((res: Car[]) => {
      return res;
    }));
  }
}
