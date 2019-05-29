import { Component, OnInit, Input } from '@angular/core';
import { Car } from 'src/app/models/car';

@Component({
  selector: 'app-choose-car-detail',
  templateUrl: './choose-car-detail.component.html',
  styleUrls: ['./choose-car-detail.component.css']
})
export class ChooseCarDetailComponent implements OnInit {
  @Input() car : Car;

  constructor() { }

  ngOnInit() {
  }

}
