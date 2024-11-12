import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FlightService } from '../../../services/flight.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PlaneService } from '../../../services/plane.service';
import { PilotService } from '../../../services/pilot.service';
import { FlightServiceFormModel } from '../../../models/flight';

@Component({
  selector: 'app-update-flight',
  templateUrl: './update-flight.component.html',
  styleUrls: ['./update-flight.component.css']
})
export class UpdateFlightComponent implements OnInit {
  flightForm: FormGroup;
  flights: FlightServiceFormModel[] = [];
  selectedFlightId: string = '';
  message: string = '';
  planes: any[] = [];
  pilots: any[] = [];
  doctors: any[] = [];
  cities: string[] = ['Cairo', 'Dubai', 'San Francisco', 'London'];
  constructor(
    private fb: FormBuilder,
    private flightService: FlightService,
    private route: ActivatedRoute,
    private router: Router,
    private planeService: PlaneService,
    private pilotService: PilotService
  ) {
    this.flightForm = this.fb.group({
      departureTime: ['', Validators.required],
      arrivalTime: ['', Validators.required],
      Takeoff_Location: ['', Validators.required],
      Destination: ['', Validators.required],
      planeId: ['', Validators.required],
      pilotId: ['', Validators.required],
      doctorId: ['']
    });
  }

  ngOnInit(): void {
    this.loadFlights();
    this.loadPlanes();
    this.loadPilots();
    this.loadDoctors();
  }

  loadFlights(): void {
    this.flightService.getAllFlights().subscribe(flights => {
      this.flights = flights.map(flight => ({
        flightId: flight.flightId,
        departureTime: flight.departureTime,
        arrivalTime: flight.arrivalTime,
        Takeoff_Location: flight.Takeoff_Location,
        Destination: flight.Destination,
        planeId: flight.planeId,
        pilotId: flight.pilotId,
        doctorId: flight.doctorId,
        planeModel: flight.planeModel,   // Map additional properties
        planePayload: flight.planePayload
      }));
    });
  }
  loadPlanes() {
    this.planeService.getPlanes().subscribe({
      next: (data) => this.planes = data,
      error: (error) => console.error('Failed to fetch planes', error),
    });
  }

  loadPilots() {
    const pilotRole = 1; // Replace with the appropriate role ID for pilots
    this.pilotService.getPilotsByRole(pilotRole).subscribe({
      next: (data) => this.pilots = data,
      error: (error) => console.error('Failed to fetch pilots', error),
    });
  }

  loadDoctors() {
    const doctorRole = 4; // Role ID for doctors
    this.pilotService.getPilotsByRole(doctorRole).subscribe({
      next: (data) => this.doctors = data,
      error: (error) => console.error('Failed to fetch doctors', error),
    });
  }

  onFlightSelect(event: any): void {
    this.selectedFlightId = event.target.value;
    if (this.selectedFlightId) {
      this.loadFlightData(this.selectedFlightId);
    }
  }

  loadFlightData(flightId: string): void {
    this.flightService.getFlightById(flightId).subscribe(flight => {
      this.flightForm.patchValue({
        departureTime: flight.departureTime,
        arrivalTime: flight.arrivalTime,
        Takeoff_Location: flight.Takeoff_Location,
        Destination: flight.Destination,
        planeId: flight.planeId,
        pilotId: flight.pilotId,
        doctorId: flight.doctorId
      });
    });
  }

  onUpdateFlight(): void {
    if (this.flightForm.invalid) {
      this.message = 'Please fill in all required fields.';
      return;
    }

    const flightData: FlightServiceFormModel = this.flightForm.value;

    this.flightService.updateFlight(this.selectedFlightId, flightData).subscribe(
      () => {
        this.message = 'Flight updated successfully';
        this.router.navigate(['/flights']);
      },
      error => {
        this.message = `Error: ${error.message}`;
      }
    );
  }
}
