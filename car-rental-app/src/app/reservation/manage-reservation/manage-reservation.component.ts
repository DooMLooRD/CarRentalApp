import { Component, OnInit } from '@angular/core';
import { DataStorageService } from '../../services/data-storage.service';
import { ReservationService } from '../../services/reservation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-manage-reservation',
  templateUrl: './manage-reservation.component.html',
  styleUrls: ['./manage-reservation.component.css']
})
export class ManageReservationComponent implements OnInit {
  reservationInput = {
    reservationNumber: null,
    surname: ""
  }
  modalModel: {
    header: string,
    message: string
  }
  isChecking: boolean;
  isUpdating: boolean;
  isRemoving: boolean;

  constructor(private reservationService: ReservationService, private dataStorageService: DataStorageService, private router: Router, private route: ActivatedRoute, private modalService: NgbModal) { }

  ngOnInit() {
  }

  checkReservation(content) {
    this.isChecking = true;
    this.reservationService.getReservation(this.reservationInput.reservationNumber, this.reservationInput.surname).subscribe((res) => {
      this.isChecking = false;
      this.dataStorageService.setReservationSummary(res);
      this.router.navigate(['rentDetail'], { relativeTo: this.route });
    }, (error: HttpErrorResponse) => {
      if (error.status == 404) {
        this.isChecking = false;
        this.modalModel = {
          header: "Error",
          message: "There is no reservation with given surname and reservation number"
        };
        this.modalService.open(content);
      }
    });
  }
  removeReservation(content) {
    this.isRemoving = true;
    this.reservationService.removeReservation(this.reservationInput.reservationNumber, this.reservationInput.surname).subscribe((res) => {
      this.isRemoving = false;

      this.modalModel = {
        header: "Success",
        message: "Reservation removed"
      };
      this.modalService.open(content);
    }, (error: HttpErrorResponse) => {
      this.isRemoving = false;
      if (error.status == 404) {
        this.modalModel = {
          header: "Error",
          message: "There is no reservation with given surname and reservation number"
        };
        this.modalService.open(content);
      }
    })
  }
  updateReservation(content) {
    this.isUpdating = true;

    this.reservationService.getReservation(this.reservationInput.reservationNumber, this.reservationInput.surname).subscribe((res) => {
      this.isUpdating = false;
      this.dataStorageService.setReservationSummary(res);
      this.router.navigate(['editRent'], { relativeTo: this.route });
    }, (error: HttpErrorResponse) => {
      this.isUpdating = false;
      if (error.status == 404) {
        this.modalModel = {
          header: "Error",
          message: "There is no reservation with given surname and reservation number"
        };
        this.modalService.open(content);
      }
    })
  }
}
