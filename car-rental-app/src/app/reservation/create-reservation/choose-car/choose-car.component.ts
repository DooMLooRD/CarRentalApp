import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { CarService } from 'src/app/services/car.service';
import { Car } from 'src/app/models/car';

@Component({
  selector: 'app-choose-car',
  templateUrl: './choose-car.component.html',
  styleUrls: ['./choose-car.component.css']
})
export class ChooseCarComponent implements OnInit, OnChanges {

  @Input() model: {
    pickUpDate: Date,
    returnDate: Date,
    selectedCar: Car,
    updateMode: boolean,
    reservationNumber: number
  }
  isLoading: boolean;
  cars: Car[];
  constructor(private carService: CarService) { }

  ngOnInit() {
    this.isLoading = true;
    if (this.model.updateMode) {
      this.carService.getAvailableCarsForReservation(this.model.pickUpDate, this.model.returnDate, this.model.reservationNumber).subscribe((res: Car[]) => {
        this.cars = res;
        this.isLoading = false;
      });
    }
    else {
      this.carService.getAvailableCars(this.model.pickUpDate, this.model.returnDate).subscribe((res: Car[]) => {
        this.cars = res;
        this.isLoading = false;
      });
    }

  }
  ngOnChanges() {
    this.isLoading = true;
    if (this.model.updateMode) {
      this.carService.getAvailableCarsForReservation(this.model.pickUpDate, this.model.returnDate, this.model.reservationNumber).subscribe((res: Car[]) => {
        this.cars = res;
        this.isLoading = false;
      });
    }
    else {
      this.carService.getAvailableCars(this.model.pickUpDate, this.model.returnDate).subscribe((res: Car[]) => {
        this.cars = res;
        this.isLoading = false;

      });
    }

  }


}
