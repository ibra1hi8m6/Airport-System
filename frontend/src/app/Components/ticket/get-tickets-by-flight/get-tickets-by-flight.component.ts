import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../../services/ticket.service';
import { FlightService } from '../../../services/flight.service';

@Component({
  selector: 'app-get-tickets-by-flight',
  templateUrl: './get-tickets-by-flight.component.html',
  styleUrls: ['./get-tickets-by-flight.component.css']
})
export class GetTicketsByFlightComponent implements OnInit {
  flights: any[] = [];
  selectedFlightId: string | null = null;
  tickets: any[] = [];
  errorMessage: string | null = null;

  constructor(private ticketService: TicketService, private flightService: FlightService) {}

  ngOnInit(): void {
    this.loadFlights();
  }

  loadFlights(): void {
    this.flightService.getAllFlights().subscribe(
      (flights) => {
        this.flights = flights;
      },
      (error) => {
        console.error('Error fetching flights', error);
        this.errorMessage = 'Failed to load flights. Please try again later.';
      }
    );
  }

  getTickets(): void {
    if (!this.selectedFlightId) {
      this.errorMessage = "Please select a flight.";
      return;
    }

    this.ticketService.getTicketsByFlight(this.selectedFlightId).subscribe(
      (tickets) => {
        this.tickets = tickets;
        this.errorMessage = null;
      },
      (error) => {
        console.error('Error fetching tickets by flight', error);
        this.errorMessage = 'Failed to load tickets. Please try again later.';
      }
    );
  }
}
