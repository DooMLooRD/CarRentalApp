import { Component, OnInit } from '@angular/core';
import { ReservationDetail } from '../../models/reservation';
import { DataStorageService } from '../../services/data-storage.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation-summary',
  templateUrl: './reservation-summary.component.html',
  styleUrls: ['./reservation-summary.component.css']
})
export class ReservationSummaryComponent implements OnInit {
  reservation : ReservationDetail;
  isLoaded: boolean;
  manageReservation: boolean;
  constructor(private dataStorageService: DataStorageService, private route :ActivatedRoute) {
    this.manageReservation=this.route.snapshot.data['manageReservation'];
   }

  ngOnInit() {
    this.isLoaded=false;
    this.reservation=this.dataStorageService.getReservationSummary();
    this.isLoaded=true;

  }

}
