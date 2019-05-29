import { Component, OnInit } from '@angular/core';
import { Car } from '../../models/car';
import { Location } from '../../models/location';
import { Reservation, InitReservation, ReservationDetail, detailToReservation } from '../../models/reservation';
import { LocationService } from '../../services/location.service';
import { ReservationService } from '../../services/reservation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataStorageService } from '../../services/data-storage.service';

@Component({
  selector: 'app-create-reservation',
  templateUrl: './create-reservation.component.html',
  styleUrls: ['./create-reservation.component.css']
})
export class CreateReservationComponent implements OnInit {
  updateMode: boolean;
  locations: Location[];
  isCreating: boolean;
  isUpdating: boolean;
  returnLocationSameAsPickUp: boolean;
  reservation: Reservation = InitReservation();
  model = {
    pickUpDate: new Date(),
    returnDate: new Date(),
    selectedCar: new Car(),
    updateMode: this.updateMode,
    reservationNumber: this.reservation.reservationNumber
  }

  constructor(private locationService: LocationService, private reservationService: ReservationService, private dataStorageService: DataStorageService, private router: Router, private route: ActivatedRoute) {
    this.updateMode = this.route.snapshot.data['updateMode'];
    if (this.updateMode) {
      this.reservation = detailToReservation(this.dataStorageService.getReservationSummary());
      this.updateModel();
    }
  }

  ngOnInit() {
    this.locationService.getAllLocations().subscribe((res: Location[]) => {
      this.locations = res;
    });
  }
  updateModel() {
    this.model = {
      pickUpDate: this.reservation.pickUpDate,
      returnDate: this.reservation.returnDate,
      selectedCar: new Car(),
      updateMode: this.updateMode,
      reservationNumber: this.reservation.reservationNumber
    }
  }
  makeReservation() {
    this.isCreating = true;
    this.reservation.carId = this.model.selectedCar.id;
    if (this.returnLocationSameAsPickUp)
      this.reservation.returnLocationId = this.reservation.pickUpLocationId;
    this.reservationService.rentCar(this.reservation).subscribe((res: ReservationDetail) => {
      this.isCreating = false;
      this.dataStorageService.setReservationSummary(res);
      this.router.navigate(['/rentSummary']);
    });
  }
  updateReservation() {
    this.isUpdating = true;
    this.reservation.carId = this.model.selectedCar.id;
    if (this.returnLocationSameAsPickUp)
      this.reservation.returnLocationId = this.reservation.pickUpLocationId;
    this.reservationService.updateReservation(this.reservation.reservationNumber, this.reservation.surname, this.reservation).subscribe((res: ReservationDetail) => {
      this.isUpdating = false;
      this.dataStorageService.setReservationSummary(res);
      this.router.navigate(['/rentSummary']);
    });
  }
}
