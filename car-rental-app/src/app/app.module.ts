import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { NgbButtonsModule, NgbModalModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { CarsComponent } from './cars/cars.component';
import { CarDetailComponent } from './cars/car-detail/car-detail.component';
import { CreateReservationComponent } from './reservation/create-reservation/create-reservation.component';
import { ChooseCarComponent } from './reservation/create-reservation/choose-car/choose-car.component';
import { ChooseCarDetailComponent } from './reservation/create-reservation/choose-car/choose-car-detail/choose-car-detail.component';
import { ReservationSummaryComponent } from './reservation/reservation-summary/reservation-summary.component';
import { ManageReservationComponent } from './reservation/manage-reservation/manage-reservation.component';


const appRoutes: Routes = [
  { path: "", component: CarsComponent },
  { path: "cars", component: CarsComponent },
  { path: "rent", component: CreateReservationComponent },
  { path: "rentSummary", component: ReservationSummaryComponent },
  {
    path: "manageRent", component: ManageReservationComponent, children: [
      { path: "rentDetail", component: ReservationSummaryComponent, data: { manageReservation: true } },
      { path: "editRent", component: CreateReservationComponent, data: { updateMode: true } },
    ]
  },
];

@NgModule({
  declarations: [
    AppComponent,
    CarsComponent,
    CarDetailComponent,
    CreateReservationComponent,
    ChooseCarComponent,
    ChooseCarDetailComponent,
    ReservationSummaryComponent,
    ManageReservationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    NgbButtonsModule,
    NgbModalModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    NgbTooltipModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
