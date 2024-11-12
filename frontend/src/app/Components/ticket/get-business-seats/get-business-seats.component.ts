import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../../services/ticket.service';

@Component({
  selector: 'app-get-business-seats',
  templateUrl: './get-business-seats.component.html',
  styleUrls: ['./get-business-seats.component.css']
})
export class GetBusinessSeatsComponent implements OnInit {
  businessSeats: any[] = [];
  passengerDetails: { [key: string]: any } = {};
  errorMessage: string | null = null;

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.loadBusinessSeats();
  }

  loadBusinessSeats(): void {
    this.ticketService.getBusinessSeats().subscribe(
      seats => {
        this.businessSeats = seats;
        this.fetchPassengerDetails();
      },
      error => {
        console.error('Error fetching business seats', error);
        this.errorMessage = 'Failed to load business seats. Please try again later.';
      }
    );
  }

  fetchPassengerDetails(): void {
    this.businessSeats.forEach(seat => {
      const userId = seat.passengerUserId;
      if (userId && !this.passengerDetails[userId]) { // Check if details are already fetched
        this.ticketService.getUserById(userId).subscribe(
          user => {
            this.passengerDetails[userId] = user;
          },
          error => {
            console.error('Error fetching user details', error);
            this.passengerDetails[userId] = { fullName: 'Unknown Passenger', email: 'N/A' };
          }
        );
      }
    });
  }

  getPassengerName(userId: string): string {
    return this.passengerDetails[userId]?.fullName || 'Loading...';
  }
}
