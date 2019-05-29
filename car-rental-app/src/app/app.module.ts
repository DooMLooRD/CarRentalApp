import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { NgbButtonsModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { CarsComponent } from './cars/cars.component';
import { CarDetailComponent } from './cars/car-detail/car-detail.component';
import { CreateReservationComponent } from './reservation/create-reservation/create-reservation.component';
import { ChooseCarComponent } from './reservation/create-reservation/choose-car/choose-car.component';
import { ChooseCarDetailComponent } from './reservation/create-reservation/choose-car/choose-car-detail/choose-car-detail.component';
import { ReservationSummaryComponent } from './reservation/reservation-summary/reservation-summary.component';


const appRoutes: Routes = [
  { path: "", component: CarsComponent },
  { path: "cars", component: CarsComponent },
  { path: "rent", component: CreateReservationComponent },
  { path: "rentSummary", component: ReservationSummaryComponent },

];

@NgModule({
  declarations: [
    AppComponent,
    CarsComponent,
    CarDetailComponent,
    CreateReservationComponent,
    ChooseCarComponent,
    ChooseCarDetailComponent,
    ReservationSummaryComponent
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
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
