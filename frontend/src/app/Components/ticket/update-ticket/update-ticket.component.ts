import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../../services/ticket.service';
import { FlightService } from '../../../services/flight.service';
import { FlightServiceFormModel } from '../../../models/flight'; // Correct import
import { Ticket } from '../../../models/ticket.model';

@Component({
  selector: 'app-update-ticket',
  templateUrl: './update-ticket.component.html',
  styleUrls: ['./update-ticket.component.css']
})
export class UpdateTicketComponent implements OnInit {
  tickets: Ticket[] = []; // To store tickets
  selectedTicketId: string | null = null;
  ticket: Ticket | null = null; // Adjusted to use Ticket type
  errorMessage: string | null = null;
  flights: FlightServiceFormModel[] = []; // To store flights

  constructor(
    private ticketService: TicketService,
    private flightService: FlightService
  ) {}

  ngOnInit(): void {
    this.loadTickets();
    this.loadFlights();
  }

  loadTickets(): void {
    this.ticketService.getAllTickets().subscribe(
      (tickets) => {
        this.tickets = tickets;
      },
      (error) => {
        console.error('Error fetching tickets:', error);
        this.errorMessage = 'Failed to load tickets. Please try again later.';
      }
    );
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

  selectTicket(): void {
    if (this.selectedTicketId) {
      this.ticketService.getTicketById(this.selectedTicketId).subscribe(
        (ticket) => {
          this.ticket = ticket;
        },
        (error) => {
          console.error('Error fetching ticket details', error);
          this.errorMessage = 'Failed to load ticket details. Please try again later.';
        }
      );
    }
  }

  updateTicket(): void {
    if (this.selectedTicketId && this.ticket) {
      this.ticketService.updateTicket(this.selectedTicketId, this.ticket).subscribe(
        () => {
          alert('Ticket updated successfully!');
        },
        (error) => {
          console.error('Error updating ticket', error);
          this.errorMessage = 'Failed to update ticket. Please try again later.';
        }
      );
    }
  }
}
