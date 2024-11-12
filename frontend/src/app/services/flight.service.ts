import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { FlightServiceFormModel, FlightResponseModel } from '../models/flight';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class FlightService {
  private baseUrl = `${environment.apiurl}/Flight`; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  getAllFlights(): Observable<FlightServiceFormModel[]> {
    return this.http.get<FlightServiceFormModel[]>(`${this.baseUrl}/GetAllFlights`)
      .pipe(catchError(this.handleError));
  }

  getFlightById(id: string): Observable<FlightServiceFormModel> {
    return this.http.get<FlightServiceFormModel>(`${this.baseUrl}/GetFlightById/${id}`)
      .pipe(catchError(this.handleError));
  }

  createFlight(flight: FlightServiceFormModel): Observable<FlightResponseModel> {
    return this.http.post<FlightResponseModel>(`${this.baseUrl}/CreateFlight`, flight)
      .pipe(catchError(this.handleError));
  }

  updateFlight(id: string, flight: FlightServiceFormModel): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/UpdateFlight/${id}`, flight)
      .pipe(catchError(this.handleError));
  }

  deleteFlight(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteFlight/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    // Handle errors here
    return throwError('Something went wrong; please try again later.');
  }
  getFlightsByLocations(takeoffLocation: string, destination: string): Observable<FlightResponseModel[]> {
    return this.http.get<FlightResponseModel[]>(`${this.baseUrl}/GetFlightsByLocations`, {
      params: {
        takeoffLocation: takeoffLocation,
        destination: destination
      }
    });
  }
  getFlightsByDurationLessThan(hours: number): Observable<FlightResponseModel[]> {
    return this.http.get<FlightResponseModel[]>(`${this.baseUrl}/GetFlightsWithDurationLessThan/${hours}`)
      .pipe(catchError(this.handleError));
  }
  getFlightsByDurationGreaterThan(hours: number): Observable<FlightResponseModel[]> {
    return this.http.get<FlightResponseModel[]>(`${this.baseUrl}/GetFlightsWithDurationGreaterThan/${hours}`)
      .pipe(catchError(this.handleError));
  }
}
