import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Location } from '../models/location';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

constructor(private http: HttpClient) { }

getAllLocations() : Observable<Location[]>{
  return this.http.get("https://localhost:44337/api/rent/locations").pipe(map((res: Location[]) => {
    return res;
  }));
}

}
