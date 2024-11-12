import { Component, OnInit } from '@angular/core';
import { FlightService } from '../../../app/services/flight.service';
import { PlaneService } from '../../../app/services/plane.service';
import { PilotService } from '../../../app/services/pilot.service';
import {
  FlightServiceFormModel,
  FlightResponseModel,
} from '../../../app/models/flight';

@Component({
  selector: 'app-flight-create',
  templateUrl: './flight-create.component.html',
  styleUrls: ['./flight-create.component.css'],
})
export class FlightCreateComponent implements OnInit {
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
    console.log('Flight created', this.flightModel);
    this.flightService.createFlight(this.flightModel).subscribe({
      next: (response) => {
        console.log('Flight created successfully', response);
        // Refresh the flight list after creating a flight
      },
      error: (error) => {
        console.error('Failed to create flight', error);
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
