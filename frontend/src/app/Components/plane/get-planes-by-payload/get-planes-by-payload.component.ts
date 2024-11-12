import { Component } from '@angular/core';
import { PlaneService } from '../../../services/plane.service';
import { Plane } from '../../../models/plane.model';

@Component({
  selector: 'app-get-planes-by-payload',
  templateUrl: './get-planes-by-payload.component.html',
  styleUrls: ['./get-planes-by-payload.component.css']
})
export class GetPlanesByPayloadComponent {
  payload: number | null = null;  // To store the user input
  planes: Plane[] = [];  // To store the fetched planes
  errorMessage: string = '';  // To store error messages

  constructor(private planeService: PlaneService) {}

  // Method to fetch planes by payload
  getPlanesByPayload() {
    if (this.payload === null) {
      this.errorMessage = 'Please enter a valid payload value.';
      return;
    }
    this.planeService.getPlanesByPayload(this.payload).subscribe(
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
