import { Component } from '@angular/core';
import { FlightService } from '../../../services/flight.service';
import { FlightResponseModel } from '../../../models/flight';

@Component({
  selector: 'app-get-flights-with-duration-greater-than',
  templateUrl: './get-flights-with-duration-greater-than.component.html',
  styleUrls: ['./get-flights-with-duration-greater-than.component.css']
})
export class GetFlightsWithDurationGreaterThanComponent {
  duration!: number; // Duration input bound to the form
  flights: FlightResponseModel[] = []; // Initialize as an empty array

  constructor(private flightService: FlightService) {}

  onGetFlightsWithDurationGreaterThan() {
    if (this.duration) {
      this.flightService.getFlightsByDurationGreaterThan(this.duration).subscribe(
        (data: FlightResponseModel[]) => {
          this.flights = data; // Assign the response data to the flights property
        },
        error => {
          console.error('Error fetching flights:', error);
        }
      );
    }
  }
}
