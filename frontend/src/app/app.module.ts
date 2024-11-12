import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';

import { FlightListComponent } from '../Garbage/flight/flight-list/flight-list.component';
import { FlightCreateComponent } from '../Garbage/flight/flight-create/flight-create.component';
import { FlightService } from './services/flight.service';
import { HomeComponent } from './Components/home/home.component';
import { RegisterComponent } from './Components/register/register.component';
import { LoginComponent } from './Components/login/login.component';
import { FlightComponent } from './Components/flight/flight.component';
import { GateComponent } from './Components/gate/gate.component';
import { PlaneComponent } from './Components/plane/plane.component';
import { TicketComponent } from './Components/ticket/ticket.component';
import { CreateFlightComponent } from './Components/flight/create-flight/create-flight.component';

import { GetFlightsWithDoctorsComponent } from './Components/flight/get-flights-with-doctors/get-flights-with-doctors.component';
import { GetFlightsWithDurationGreaterThanComponent } from './Components/flight/get-flights-with-duration-greater-than/get-flights-with-duration-greater-than.component';
import { GetFlightsWithDurationLessThanComponent } from './Components/flight/get-flights-with-duration-less-than/get-flights-with-duration-less-than.component';
import { UpdateFlightComponent } from './Components/flight/update-flight/update-flight.component';
import { DeleteFlightComponent } from './Components/flight/delete-flight/delete-flight.component';
import { GetFlightsByPilotIdComponent } from './Components/flight/get-flights-by-pilot-id/get-flights-by-pilot-id.component';
import { GetFlightsByLocationsComponent } from './Components/flight/get-flights-by-locations/get-flights-by-locations.component';
import { CreateGateComponent } from './Components/gate/create-gate/create-gate.component';

import { GetAllGatesComponent } from './Components/gate/get-all-gates/get-all-gates.component';
import { UpdateGateComponent } from './Components/gate/update-gate/update-gate.component';
import { DeleteGateComponent } from './Components/gate/delete-gate/delete-gate.component';
import { CreatePlaneComponent } from './Components/plane/create-plane/create-plane.component';
import { GetPlaneByIdComponent } from './Components/plane/get-plane-by-id/get-plane-by-id.component';
import { GetAllPlanesComponent } from './Components/plane/get-all-planes/get-all-planes.component';
import { UpdatePlaneComponent } from './Components/plane/update-plane/update-plane.component';
import { UpdateEvenPayloadComponent } from './Components/plane/update-even-payload/update-even-payload.component';
import { DeletePlaneComponent } from './Components/plane/delete-plane/delete-plane.component';
import { GetPlanesByPayloadComponent } from './Components/plane/get-planes-by-payload/get-planes-by-payload.component';
import { GetPlanesBySeatsComponent } from './Components/plane/get-planes-by-seats/get-planes-by-seats.component';
import { CreateTicketComponent } from './Components/ticket/create-ticket/create-ticket.component';

import { UpdateTicketComponent } from './Components/ticket/update-ticket/update-ticket.component';
import { DeleteTicketComponent } from './Components/ticket/delete-ticket/delete-ticket.component';
import { GetEconomySeatsComponent } from './Components/ticket/get-economy-seats/get-economy-seats.component';
import { GetBusinessSeatsComponent } from './Components/ticket/get-business-seats/get-business-seats.component';
import { GetTicketsByFlightComponent } from './Components/ticket/get-tickets-by-flight/get-tickets-by-flight.component';
import { GetTicketsByDurationComponent } from './Components/ticket/get-tickets-by-duration/get-tickets-by-duration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TokenInterceptor } from './interceptors/token.service';
import { UpdateUserComponent } from './Components/user-management/update-user/update-user.component';
import { DeleteUserComponent } from './Components/user-management/delete-user/delete-user.component';
@NgModule({
  declarations: [
    AppComponent,
    FlightListComponent,
    FlightCreateComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    FlightComponent,
    GateComponent,
    PlaneComponent,
    TicketComponent,
    CreateFlightComponent,

    GetFlightsWithDoctorsComponent,
    GetFlightsWithDurationGreaterThanComponent,
    GetFlightsWithDurationLessThanComponent,
    UpdateFlightComponent,
    DeleteFlightComponent,
    GetFlightsByPilotIdComponent,
    GetFlightsByLocationsComponent,
    CreateGateComponent,

    GetAllGatesComponent,
    UpdateGateComponent,
    DeleteGateComponent,
    CreatePlaneComponent,
    GetPlaneByIdComponent,
    GetAllPlanesComponent,
    UpdatePlaneComponent,
    UpdateEvenPayloadComponent,
    DeletePlaneComponent,
    GetPlanesByPayloadComponent,
    GetPlanesBySeatsComponent,
    CreateTicketComponent,

    UpdateTicketComponent,
    DeleteTicketComponent,
    GetEconomySeatsComponent,
    GetBusinessSeatsComponent,
    GetTicketsByFlightComponent,
    GetTicketsByDurationComponent,
    UpdateUserComponent,
    DeleteUserComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [FlightService, { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
