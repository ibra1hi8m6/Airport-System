import { Component } from '@angular/core';
import { PlaneService } from '../../../services/plane.service';
import { Plane } from '../../../models/plane.model';
import { PlaneResponseModel } from '../../../models/plane.model';
@Component({
  selector: 'app-create-plane',
  templateUrl: './create-plane.component.html',
  styleUrls: ['./create-plane.component.css']
})
export class CreatePlaneComponent {
  planeModel!: string;
  planePayload!: number;
  seatsEconomy!: number;
  seatsBusiness!: number;
  message: string = '';

  constructor(private planeService: PlaneService) {}

  onSubmit() {
    const plane: Plane = {
      id: '', // You might need to handle the ID separately or let the backend assign it
      plane_model: this.planeModel,
      plane_Payload: this.planePayload,
      seats_Economy: this.seatsEconomy,
      seats_Business: this.seatsBusiness
    };

    this.planeService.createPlane(plane).subscribe(
      (response: PlaneResponseModel) => {
        // Handle optional message properly
        this.message = response.message ?? '';
      },
      error => {
        this.message = 'Error creating plane: ' + error.message;
      }
    );
  }
}
