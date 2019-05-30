import { Component, OnInit } from '@angular/core';
import { CarService } from '../services/car.service';
import { Car } from '../models/car';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {
  cars: Car[];
  isLoading: boolean;
  constructor(private carService: CarService) { }

  ngOnInit() {
    this.isLoading = true;
    this.carService.getAllCars().subscribe((res) => {
      this.cars = res;
      this.isLoading = false;
    });
  }

}
