export interface Plane {
  id: string;
  plane_model: string;
  plane_Payload: number;
  seats_Economy: number;
  seats_Business: number;
}

export interface PlaneResponseModel {
  planeId: string;
  planeModel: string;
  planePayload: number;
  seatsEconomy: number;
  seatsBusiness: number;
  message?: string; // Optional message
}
