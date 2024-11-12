import { Component } from '@angular/core';
import { TicketService } from '../../../services/ticket.service';

@Component({
  selector: 'app-get-tickets-by-duration',
  templateUrl: './get-tickets-by-duration.component.html',
  styleUrls: ['./get-tickets-by-duration.component.css']
})
export class GetTicketsByDurationComponent {
  duration: number = 0;
  tickets: any[] = [];
  errorMessage: string | null = null;

  constructor(private ticketService: TicketService) {}

  getTickets(): void {
    if (this.duration <= 0) {
      this.errorMessage = "Please enter a valid duration.";
      return;
    }

    this.ticketService.getTicketsByDuration(this.duration).subscribe(
      (tickets) => {
        this.tickets = tickets;
        this.errorMessage = null;
      },
      (error) => {
        console.error('Error fetching tickets by duration', error);
        this.errorMessage = 'Failed to load tickets. Please try again later.';
      }
    );
  }
}
