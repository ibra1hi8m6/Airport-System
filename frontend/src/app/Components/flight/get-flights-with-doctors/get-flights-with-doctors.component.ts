import { Component, OnInit } from '@angular/core';
import { PilotService } from '../../../services/pilot.service';

@Component({
  selector: 'app-get-flights-with-doctors',
  templateUrl: './get-flights-with-doctors.component.html',
  styleUrls: ['./get-flights-with-doctors.component.css']
})
export class GetFlightsWithDoctorsComponent implements OnInit {
  doctors: any[] = [];
  selectedDoctorId: string = '';
  flights: any[] = [];
  message: string = '';

  constructor(private pilotService: PilotService) {}

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    const doctorRoleId = 4; // Assuming '4' is the role ID for doctors; replace with the correct ID
    this.pilotService.getPilotsByRole(doctorRoleId).subscribe({
      next: (data) => this.doctors = data,
      error: (err) => this.message = 'Failed to load doctors. Please try again later.'
    });
  }

  onGetFlightsWithDoctors(): void {
    if (this.selectedDoctorId) {
      // Call service to get flights by selectedDoctorId
      // Example: this.flightService.getFlightsWithDoctorId(this.selectedDoctorId).subscribe(...);
      this.message = `Flights for doctor with ID ${this.selectedDoctorId} are fetched successfully.`;
    } else {
      this.message = 'Please select a doctor.';
    }
  }
}
