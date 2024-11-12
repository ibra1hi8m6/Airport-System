export interface FlightServiceFormModel {
  flightId?: string;
  departureTime: Date;
  arrivalTime: Date;
  Takeoff_Location: string;
  Destination: string;
  planeId: string;
  pilotId: string;
  doctorId?: string;
  planeModel?: string;  // Add other properties as needed
  planePayload?: string;
}

export interface FlightResponseModel {
  flightId: string;
  departureTime: Date;
  arrivalTime: Date;
  takeoff_Location: string;
  destination: string;
  planeId: string;
  planeModel: string;
  planePayload: number;
}
export interface Pilot {
  id: string;
  fullName: string;
}
