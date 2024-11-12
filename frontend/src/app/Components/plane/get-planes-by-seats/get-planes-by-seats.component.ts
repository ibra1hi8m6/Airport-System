import { Component } from '@angular/core';
import { PlaneService } from '../../../services/plane.service';
import { Plane } from '../../../models/plane.model';

@Component({
  selector: 'app-get-planes-by-seats',
  templateUrl: './get-planes-by-seats.component.html',
  styleUrls: ['./get-planes-by-seats.component.css']
})
export class GetPlanesBySeatsComponent {
  seats: number | null = null;  // To store the user input
  planes: Plane[] = [];  // To store the fetched planes
  errorMessage: string = '';  // To store error messages

  constructor(private planeService: PlaneService) {}

  // Method to fetch planes by number of seats
  getPlanesBySeats() {
    if (this.seats === null) {
      this.errorMessage = 'Please enter a valid number of seats.';
      return;
    }
    this.planeService.getPlanesBySeats(this.seats).subscribe(
      (planes: Plane[]) => {
        this.planes = planes;
        this.errorMessage = '';  // Clear any previous error message
      },
      error => {
        this.errorMessage = 'Error fetching planes: ' + error.message;
      }
    );
  }
}
