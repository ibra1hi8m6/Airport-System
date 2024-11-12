export interface Ticket {
  id?: string; // GUID in backend will be a string in Angular
  ticketClass: number; // Enum mapping
  PassengerPayload: number;
  seatNumber: string;
  gateId: string;
  passengerId: string;
  ticketCashierId: string;
  flightId: string;
  passengerUser?: { // Add this if passengerUser exists in the ticket response
    firstName: string;
    lastName: string;
    // Add other properties as needed
  };
  canUpdate?: boolean; // Optional properties
  canRegisterExtraPayload?: boolean; // Optional properties
}
