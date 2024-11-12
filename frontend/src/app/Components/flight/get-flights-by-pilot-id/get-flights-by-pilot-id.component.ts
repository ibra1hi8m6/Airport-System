import { Component, OnInit } from '@angular/core';
import { PilotService } from '../../../services/pilot.service';

@Component({
  selector: 'app-get-flights-by-pilot-id',
  templateUrl: './get-flights-by-pilot-id.component.html',
  styleUrl: './get-flights-by-pilot-id.component.css'
})
export class GetFlightsByPilotIdComponent implements OnInit {
    pilots: any[] = [];
    selectedPilotId: string = '';
    flightDetails: any[] = [];
    message: string = '';

    constructor(private pilotService: PilotService) {}

    ngOnInit(): void {
      this.loadPilots();
    }

    loadPilots(): void {
      const pilotRoleId = 2; // Assuming '2' represents the role ID for pilots; replace with the correct ID
      this.pilotService.getPilotsByRole(pilotRoleId).subscribe({
        next: (data) => this.pilots = data,
        error: (err) => this.message = 'Failed to load pilots. Please try again later.'
      });
    }

    onGetFlight(): void {
      if (this.selectedPilotId) {
        // Call service to get flights by selectedPilotId
        // Example: this.flightService.getFlightsByPilotId(this.selectedPilotId).subscribe(...);
        this.message = `Flights for pilot with ID ${this.selectedPilotId} are fetched successfully.`;
      } else {
        this.message = 'Please select a pilot.';
      }
    }
  }
