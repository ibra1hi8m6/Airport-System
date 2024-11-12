import { Component } from '@angular/core';
import { FlightService } from '../../../services/flight.service';
import { FlightResponseModel } from '../../../models/flight';

@Component({
  selector: 'app-get-flights-with-duration-less-than',
  templateUrl: './get-flights-with-duration-less-than.component.html',
  styleUrls: ['./get-flights-with-duration-less-than.component.css']
})
export class GetFlightsWithDurationLessThanComponent {
  duration: number | undefined; // This is used to bind to the input field
  flights: FlightResponseModel[] = []; // Initialize as an empty array

  constructor(private flightService: FlightService) {}

  onGetFlightsWithDurationLessThan() {
    if (this.duration) {
      this.flightService.getFlightsByDurationLessThan(this.duration).subscribe(
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
