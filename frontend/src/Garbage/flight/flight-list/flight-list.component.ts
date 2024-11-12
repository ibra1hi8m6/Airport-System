import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../../app/services/flight.service';
import { FlightResponseModel } from '../../../app/models/flight';

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  styleUrls: [],
})
export class FlightListComponent implements OnInit {
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
