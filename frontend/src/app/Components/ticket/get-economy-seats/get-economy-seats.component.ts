import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../../services/ticket.service';

@Component({
  selector: 'app-get-economy-seats',
  templateUrl: './get-economy-seats.component.html',
  styleUrls: ['./get-economy-seats.component.css'] // Ensure 'styleUrls' is correct
})
export class GetEconomySeatsComponent implements OnInit {
  economySeats: any[] = [];
  passengerDetails: { [key: string]: any } = {};
  errorMessage: string | null = null;

  constructor(private ticketService: TicketService) { }

  ngOnInit(): void {
    this.ticketService.getEconomySeats().subscribe(
      seats => {
        this.economySeats = seats;
        this.fetchPassengerDetails();
      },
      error => {
        this.errorMessage = 'Failed to load economy seats';
      }
    );
  }

  fetchPassengerDetails(): void {
    this.economySeats.forEach(seat => {
      const userId = seat.passengerUserId;
      if (userId) {
        this.ticketService.getUserById(userId).subscribe(
          user => {
            this.passengerDetails[userId] = user;
          },
          error => {
            this.passengerDetails[userId] = { FullName: 'Unknown Passenger', Email: 'N/A' };
          }
        );
      }
    });
  }
  loadEconomySeats(): void {
    this.ticketService.getEconomySeats().subscribe(
      (seats) => {
        this.economySeats = seats;
      },
      (error) => {
        console.error('Error fetching economy seats', error);
        this.errorMessage = 'Failed to load economy seats. Please try again later.';
      }
    );
  }
  getPassengerName(userId: string): string {
    return this.passengerDetails[userId]?.fullName || 'Loading...';
  }
}
