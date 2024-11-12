import { Component } from '@angular/core';
import { TicketService } from '../../../services/ticket.service';
import { Ticket } from '../../../models/ticket.model';
import { PilotService } from '../../../services/pilot.service';
import { FlightService } from '../../../services/flight.service';
import { GateService } from '../../../services/gate.service';
@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.css']
})
export class CreateTicketComponent {
  ticketModel: Ticket = {
    ticketClass: 2, // Default value or adjust as needed
    PassengerPayload:1,
    seatNumber: '',
    gateId: '',
    passengerId: '',
    ticketCashierId: '',
    flightId: '',
    canUpdate:true,
    canRegisterExtraPayload:false

  };

  pilots: any[] = [];
  flights: any[] = [];
  gates: any[] = [];
  constructor(
    private ticketService: TicketService,
    private pilotService: PilotService,
    private flightService: FlightService,
    private gateService: GateService

  ) { }

  ngOnInit(): void {
    this.loadPilots();
    this.loadFlights();
    this.loadGates();
  }
   loadPilots(): void {
    this.pilotService.getPilotsByRole(2) // Assuming 2 is the role for passengers, adjust as needed
      .subscribe(pilots => this.pilots = pilots);
    this.pilotService.getPilotsByRole(3) // Assuming 3 is the role for ticket cashiers
      .subscribe(ticketCashiers => this.pilots = this.pilots.concat(ticketCashiers));
  }

  loadFlights(): void {
    this.flightService.getAllFlights()
      .subscribe(flights => this.flights = flights);
  }

  loadGates(): void {
    this.gateService.getAllGates()
      .subscribe(gates => this.gates = gates);
  }
  createTicket() {
    this.ticketService.createTicket(this.ticketModel).subscribe(
      response => {
        console.log('Ticket created successfully', response);
        // Handle success (e.g., show a success message or navigate to another page)
      },
      error => {
        console.error('Error creating ticket', error);
        // Handle error (e.g., show an error message)
      }
    );
  }


}
