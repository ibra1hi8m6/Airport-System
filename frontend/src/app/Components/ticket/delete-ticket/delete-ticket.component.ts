import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../../services/ticket.service'; // Adjust the path as necessary
import { Ticket } from '../../../models/ticket.model'; // Adjust the path as necessary

@Component({
  selector: 'app-delete-ticket',
  templateUrl: './delete-ticket.component.html',
  styleUrls: ['./delete-ticket.component.css'] // Fix the styleUrl to styleUrls
})
export class DeleteTicketComponent implements OnInit {
  tickets: Ticket[] = [];
  selectedTicketId: string | null = null;
  message: string | null = null;

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.getAllTickets().subscribe(
      (tickets) => {
        this.tickets = tickets;
      },
      (error) => {
        console.error('Error fetching tickets:', error);
      }
    );
  }

  deleteTicket(): void {
    if (this.selectedTicketId) {
      this.ticketService.deleteTicket(this.selectedTicketId).subscribe(
        () => {
          this.message = 'Ticket deleted successfully!';
          this.loadTickets(); // Refresh the list of tickets
        },
        (error) => {
          console.error('Error deleting ticket:', error);
          this.message = 'Failed to delete the ticket.';
        }
      );
    } else {
      this.message = 'Please select a ticket to delete.';
    }
  }
}
