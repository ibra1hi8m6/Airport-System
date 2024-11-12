import { Component, OnInit } from '@angular/core';
import { PlaneService } from '../../../services/plane.service';
import { Plane, PlaneResponseModel } from '../../../models/plane.model';

@Component({
  selector: 'app-update-even-payload',  // Ensure the selector matches your component
  templateUrl: './update-even-payload.component.html',  // Adjust to match your component name
  styleUrls: ['./update-even-payload.component.css']
})
export class UpdateEvenPayloadComponent implements OnInit {
  planes: Plane[] = [];
  selectedPlaneId: string = '';
  planeModel: string = '';
  planePayload: number | null = null;
  seatsEconomy: number | null = null;
  seatsBusiness: number | null = null;
  message: string = '';

  constructor(private planeService: PlaneService) {}

  ngOnInit() {
    this.loadPlanes();
  }

  loadPlanes() {
    this.planeService.getPlanes().subscribe(
      (planes: Plane[]) => {
        this.planes = planes;
      },
      error => {
        this.message = 'Error loading planes: ' + error.message;
      }
    );
  }

  onPlaneSelect(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const id = selectElement.value;
    this.planeService.getPlaneById(id).subscribe(
      (plane: Plane) => {
        console.log(plane);
        this.selectedPlaneId = plane.id;
        this.planeModel = plane.plane_model;
        this.planePayload = plane.plane_Payload;
        this.seatsEconomy = plane.seats_Economy;
        this.seatsBusiness = plane.seats_Business;
      },
      error => {
        this.message = 'Error loading plane details: ' + error.message;
      }
    );
  }

  onSubmit() {
    if (this.planePayload === null || this.selectedPlaneId === '') {
      this.message = 'Please select a plane and provide all details.';
      return;
    }

    const planeForm: Plane = {
      id: this.selectedPlaneId,
      plane_model: this.planeModel,
      plane_Payload: this.planePayload,
      seats_Economy: this.seatsEconomy!,
      seats_Business: this.seatsBusiness!
    };

    this.planeService.updatePlane(this.selectedPlaneId, planeForm).subscribe(
      (response: PlaneResponseModel) => {
        this.message = `Plane updated successfully. New Payload: ${response.planePayload}`;
      },
      error => {
        this.message = 'Error updating plane: ' + error.message;
      }
    );
  }
}
