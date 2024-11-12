import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../../services/flight.service';
import { FlightServiceFormModel } from '../../../models/flight';
@Component({
  selector: 'app-delete-flight',
  templateUrl: './delete-flight.component.html',
  styleUrl: './delete-flight.component.css'
})
export class DeleteFlightComponent implements OnInit {
  flights: FlightServiceFormModel[] = [];
  selectedFlightId: string = '';
  message: string = '';

  constructor(private flightService: FlightService) {}

  ngOnInit(): void {
    this.loadFlights();
  }

  loadFlights(): void {
    this.flightService.getAllFlights().subscribe({
      next: (data) => this.flights = data,
      error: (err) => this.message = 'Failed to load flights. Please try again later.'
    });
  }

  onDelete(): void {
    if (this.selectedFlightId) {
      this.flightService.deleteFlight(this.selectedFlightId).subscribe({
        next: () => this.message = 'Flight deleted successfully!',
        error: (err) => this.message = 'Failed to delete flight. Please try again later.'
      });
    } else {
      this.message = 'Please select a flight to delete.';
    }
  }
}
