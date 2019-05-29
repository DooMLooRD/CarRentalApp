import { Component, OnInit, Input } from '@angular/core';
import { CarService } from 'src/app/services/car.service';
import { Car } from 'src/app/models/car';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-choose-car',
  templateUrl: './choose-car.component.html',
  styleUrls: ['./choose-car.component.css']
})
export class ChooseCarComponent implements OnInit {

  @Input() model: boolean;
  @Input() reservationForm: FormGroup;

  isLoading: boolean;
  cars: Car[];

  constructor(private carService: CarService) { }

  ngOnInit() {
    this.cars = [];
    this.onChanges();
    this.updateAvailableCars();
  }
  onChanges(): void {
    this.reservationForm.get('pickUpDate').valueChanges.subscribe((val) => {
      this.updateAvailableCars();
    });
    this.reservationForm.get('returnDate').valueChanges.subscribe((val) => {
      this.updateAvailableCars();
    });

  }

  updateAvailableCars() {
    this.isLoading = true;
    if (this.model) {
      this.carService.getAvailableCarsForReservation(this.reservationForm.get('pickUpDate').value, this.reservationForm.get('returnDate').value, this.reservationForm.get('reservationNumber').value).subscribe((res: Car[]) => {
        this.cars = res;
        this.isLoading = false;
      });
    }
    else {
      this.carService.getAvailableCars(this.reservationForm.get('pickUpDate').value, this.reservationForm.get('returnDate').value).subscribe((res: Car[]) => {
        this.cars = res;
        this.isLoading = false;
      });
    }
  }
}
