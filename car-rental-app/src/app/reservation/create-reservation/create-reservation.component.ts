import { Component, OnInit } from '@angular/core';
import { Location } from '../../models/location';
import { Reservation, InitReservation, ReservationDetail, detailToReservation } from '../../models/reservation';
import { LocationService } from '../../services/location.service';
import { ReservationService } from '../../services/reservation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataStorageService } from '../../services/data-storage.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

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
  reservationForm: FormGroup;


  constructor(private locationService: LocationService, private reservationService: ReservationService, private dataStorageService: DataStorageService, private router: Router, private route: ActivatedRoute) {
    this.updateMode = this.route.snapshot.data['updateMode'];
  }

  ngOnInit() {
    this.reservationForm = new FormGroup({
      'reservationNumber': new FormControl(0),
      'surname': new FormControl("", Validators.required),
      'age': new FormControl(0, [Validators.required, Validators.min(18)]),
      'carId': new FormControl(null, Validators.required),
      'pickUpDate': new FormControl(new Date(), Validators.required),
      'returnDate': new FormControl(new Date(), Validators.required),
      'pickUpLocation': new FormControl(null, Validators.required),
      'returnLocation': new FormControl(null, Validators.required)
    });

    this.locationService.getAllLocations().subscribe((res: Location[]) => {
      this.locations = res;
      let reservation = this.updateMode ? detailToReservation(this.dataStorageService.getReservationSummary()) : InitReservation();
      if (reservation.pickUpLocationId == 0) {
        reservation.pickUpLocationId = this.locations[0].id;
        reservation.returnLocationId = this.locations[0].id;
      }
      this.initForm(reservation);
    });
  }

  initForm(reservation) {
    this.reservationForm.setValue({
      'reservationNumber': reservation.reservationNumber,
      'surname': reservation.surname,
      'age': reservation.age,
      'carId': null,
      'pickUpDate': reservation.pickUpDate,
      'returnDate': reservation.returnDate,
      'pickUpLocation': reservation.pickUpLocationId,
      'returnLocation': reservation.returnLocationId
    });
  }

  readForm(): Reservation {
    return {
      'reservationNumber': this.reservationForm.value['reservationNumber'],
      'surname': this.reservationForm.value['surname'],
      'age': this.reservationForm.value['age'],
      'carId': this.reservationForm.value['carId'],
      'pickUpDate': this.reservationForm.value['pickUpDate'],
      'returnDate': this.reservationForm.value['returnDate'],
      'pickUpLocationId': this.reservationForm.value['pickUpLocation'],
      'returnLocationId': this.reservationForm.value['returnLocation'],
    }
  }
  makeReservation() {
    this.isCreating = true;
    let reservation = this.readForm();
    if (this.returnLocationSameAsPickUp)
      reservation.returnLocationId = reservation.pickUpLocationId;
    this.reservationService.rentCar(reservation).subscribe((res: ReservationDetail) => {
      this.isCreating = false;
      this.dataStorageService.setReservationSummary(res);
      this.router.navigate(['/rentSummary']);
    });
    this.isCreating = false;
  }
  updateReservation() {
    this.isUpdating = true;
    let reservation = this.readForm();

    if (this.returnLocationSameAsPickUp)
      reservation.returnLocationId = reservation.pickUpLocationId;
    this.reservationService.updateReservation(reservation.reservationNumber, reservation.surname, reservation).subscribe((res: ReservationDetail) => {
      this.isUpdating = false;
      this.dataStorageService.setReservationSummary(res);
      this.router.navigate(['/rentSummary']);
    });
    this.isUpdating = false;

  }
}
