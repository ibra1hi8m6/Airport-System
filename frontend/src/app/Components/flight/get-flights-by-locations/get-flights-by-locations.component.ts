import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../../services/flight.service';
import { FlightResponseModel } from '../../../models/flight';

@Component({
  selector: 'app-get-flights-by-locations',
  templateUrl: './get-flights-by-locations.component.html',
  styleUrl: './get-flights-by-locations.component.css',
})
export class GetFlightsByLocationsComponent implements OnInit {
  flights: FlightResponseModel[] = [];
  selectedTakeoffLocation: string = '';
  selectedDestination: string = '';
  cities: string[] = ['Cairo', 'Dubai', 'San Francisco', 'London'];
  constructor(private flightService: FlightService) {}
  getFlights() {
    this.flightService
      .getFlightsByLocations(
        this.selectedTakeoffLocation,
        this.selectedDestination
      )
      .subscribe((response) => {
        this.flights = response;
      });
  }

  ngOnInit(): void {}
}
