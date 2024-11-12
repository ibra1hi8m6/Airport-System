import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../../services/flight.service';
import { PlaneService } from '../../../services/plane.service';
import { PilotService } from '../../../services/pilot.service';
import {
  FlightServiceFormModel,
  FlightResponseModel,
} from '../../../models/flight';
@Component({
  selector: 'app-create-flight',
  templateUrl: './create-flight.component.html',
  styleUrl: './create-flight.component.css',
})
export class CreateFlightComponent implements OnInit {
  flightModel: FlightServiceFormModel = {
    departureTime: new Date(),
    arrivalTime: new Date(),
    Takeoff_Location: '',
    Destination: '',
    planeId: '',
    pilotId: '',
    doctorId: '',
  };

  flights: FlightResponseModel[] = [];
  planes: any[] = [];
  pilots: any[] = [];
  cities: string[] = ['Cairo', 'Dubai', 'San Francisco', 'London'];
  doctors: any[] = [];
  message: string = '';

  constructor(
    private flightService: FlightService,
    private planeService: PlaneService,
    private pilotService: PilotService
  ) {}

  ngOnInit() {
    this.loadDoctors();
    this.loadPlanes();
    this.loadPilots();
  }

  onSubmit() {
    this.flightService.createFlight(this.flightModel).subscribe({
      next: (response) => {
        console.log('Flight created successfully', response);
        this.message = 'Done: Flight has been created successfully.'; // Set success message
      },
      error: (error) => {
        console.error('Failed to create flight', error);
        this.message = 'Not Done: Flight creation failed. Please try again.'; // Set failure message
      },
    });
  }

  loadPlanes() {
    this.planeService.getPlanes().subscribe({
      next: (data) => {
        console.log('Planes fetched:', data);
        this.planes = data;
      },
      error: (error) => {
        console.error('Failed to fetch planes', error);
      },
    });
  }

  loadPilots() {
    const pilotRole = 1; // Replace with the appropriate role ID for pilots
    this.pilotService.getPilotsByRole(pilotRole).subscribe({
      next: (data) => {
        this.pilots = data;
      },
      error: (error) => {
        console.error('Failed to fetch pilots', error);
      },
    });
  }

  loadDoctors() {
    const doctorRole = 4; // Role ID for doctors
    this.pilotService.getPilotsByRole(doctorRole).subscribe({
      next: (data) => {
        this.doctors = data;
      },
      error: (error) => {
        console.error('Failed to fetch doctors', error);
      },
    });
  }
}
