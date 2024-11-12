import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './Components/home/home.component'; // Import the HomeComponent
import { RegisterComponent } from './Components/register/register.component'; // Import the RegisterComponent
import { LoginComponent } from './Components/login/login.component'; // Import the LoginComponent
import { DeleteUserComponent } from './Components/user-management/delete-user/delete-user.component';
import { UpdateUserComponent } from './Components/user-management/update-user/update-user.component';


// Importing Flight Components
import { CreateFlightComponent } from './Components/flight/create-flight/create-flight.component';

import { GetFlightsWithDoctorsComponent } from './Components/flight/get-flights-with-doctors/get-flights-with-doctors.component';
import { GetFlightsWithDurationGreaterThanComponent } from './Components/flight/get-flights-with-duration-greater-than/get-flights-with-duration-greater-than.component';
import { GetFlightsWithDurationLessThanComponent } from './Components/flight/get-flights-with-duration-less-than/get-flights-with-duration-less-than.component';
import { UpdateFlightComponent } from './Components/flight/update-flight/update-flight.component';
import { DeleteFlightComponent } from './Components/flight/delete-flight/delete-flight.component';
import { GetFlightsByPilotIdComponent } from './Components/flight/get-flights-by-pilot-id/get-flights-by-pilot-id.component';
import { GetFlightsByLocationsComponent } from './Components/flight/get-flights-by-locations/get-flights-by-locations.component';

// Importing Gate Components
import { CreateGateComponent } from './Components/gate/create-gate/create-gate.component';

import { GetAllGatesComponent } from './Components/gate/get-all-gates/get-all-gates.component';
import { UpdateGateComponent } from './Components/gate/update-gate/update-gate.component';
import { DeleteGateComponent } from './Components/gate/delete-gate/delete-gate.component';

// Importing Plane Components
import { CreatePlaneComponent } from './Components/plane/create-plane/create-plane.component';

import { GetAllPlanesComponent } from './Components/plane/get-all-planes/get-all-planes.component';
import { UpdatePlaneComponent } from './Components/plane/update-plane/update-plane.component';
import { UpdateEvenPayloadComponent } from './Components/plane/update-even-payload/update-even-payload.component';
import { DeletePlaneComponent } from './Components/plane/delete-plane/delete-plane.component';
import { GetPlanesByPayloadComponent } from './Components/plane/get-planes-by-payload/get-planes-by-payload.component';
import { GetPlanesBySeatsComponent } from './Components/plane/get-planes-by-seats/get-planes-by-seats.component';

// Importing Ticket Components
import { CreateTicketComponent } from './Components/ticket/create-ticket/create-ticket.component';

import { UpdateTicketComponent } from './Components/ticket/update-ticket/update-ticket.component';
import { DeleteTicketComponent } from './Components/ticket/delete-ticket/delete-ticket.component';
import { GetEconomySeatsComponent } from './Components/ticket/get-economy-seats/get-economy-seats.component';
import { GetBusinessSeatsComponent } from './Components/ticket/get-business-seats/get-business-seats.component';
import { GetTicketsByFlightComponent } from './Components/ticket/get-tickets-by-flight/get-tickets-by-flight.component';
import { GetTicketsByDurationComponent } from './Components/ticket/get-tickets-by-duration/get-tickets-by-duration.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },

  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },// Redirect to home by default




 // Flight Routes
{path: 'flight',
children: [
  { path: 'create', component: CreateFlightComponent },
  { path: 'doctors', component: GetFlightsWithDoctorsComponent },
  { path: 'duration/greater-than', component: GetFlightsWithDurationGreaterThanComponent },
  { path: 'duration/less-than', component: GetFlightsWithDurationLessThanComponent },
  { path: 'update/:id', component: UpdateFlightComponent },
  { path: 'delete/:id', component: DeleteFlightComponent },
  { path: 'pilot/:pilotId', component: GetFlightsByPilotIdComponent },
  { path: 'locations', component: GetFlightsByLocationsComponent }
]},
// { path: 'flight/create', component: CreateFlightComponent },

// { path: 'flight/doctors', component: GetFlightsWithDoctorsComponent },
// { path: 'flight/duration/greater-than', component: GetFlightsWithDurationGreaterThanComponent },
// { path: 'flight/duration/less-than', component: GetFlightsWithDurationLessThanComponent },
// { path: 'flight/update/:id', component: UpdateFlightComponent },
// { path: 'flight/delete/:id', component: DeleteFlightComponent },
// { path: 'flight/pilot/:pilotId', component: GetFlightsByPilotIdComponent },
// { path: 'flight/locations', component: GetFlightsByLocationsComponent },

 // Gate Routes
{ path: 'gate/create', component: CreateGateComponent },

{ path: 'gate/all', component: GetAllGatesComponent },
{ path: 'gate/update/:id', component: UpdateGateComponent },
{ path: 'gate/delete/:id', component: DeleteGateComponent },

 // Plane Routes
{ path: 'plane/create', component: CreatePlaneComponent },

{ path: 'plane/all', component: GetAllPlanesComponent },
{ path: 'plane/update/:id', component: UpdatePlaneComponent },
{ path: 'plane/update-payload/:id', component: UpdateEvenPayloadComponent },
{ path: 'plane/delete/:id', component: DeletePlaneComponent },
{ path: 'plane/payload', component: GetPlanesByPayloadComponent },
{ path: 'plane/seats', component: GetPlanesBySeatsComponent },

 // Ticket Routes
{ path: 'ticket/create', component: CreateTicketComponent },

{ path: 'ticket/update/:id', component: UpdateTicketComponent },
{ path: 'ticket/delete/:id', component: DeleteTicketComponent },
{ path: 'ticket/economy-seats', component: GetEconomySeatsComponent },
{ path: 'ticket/business-seats', component: GetBusinessSeatsComponent },
{ path: 'ticket/flight/:flightId', component: GetTicketsByFlightComponent },
{ path: 'ticket/duration', component: GetTicketsByDurationComponent },

{ path: 'user/update/:id', component: UpdateUserComponent },
  { path: 'user/delete/:id', component: DeleteUserComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
