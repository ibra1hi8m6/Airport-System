import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Ticket } from '../models/ticket.model';
@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private apiUrl = `${environment.apiurl}/Tickets`;

  constructor(private http: HttpClient) { }

  createTicket(ticket: Ticket): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/CreateTicket`, ticket, this.getHttpOptions());
  }

  getTicketById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetTicketById/${id}`, this.getHttpOptions());
  }

  getAllTickets(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetAllTickets`, this.getHttpOptions());
  }

  updateTicket(id: string, ticket: Ticket): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/UpdateTicket/${id}`, ticket, this.getHttpOptions());
  }

  deleteTicket(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/DeleteTicket/${id}`, this.getHttpOptions());
  }

  getEconomySeats(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetEconomySeats`, this.getHttpOptions());
  }

  getBusinessSeats(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetBusinessSeats`, this.getHttpOptions());
  }

  getTicketsByFlight(flightId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetTicketsByFlight/${flightId}`, this.getHttpOptions());
  }

  getTicketsByDuration(hours: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetTicketsByDuration?hours=${hours}`, this.getHttpOptions());
  }
  getUserById(userId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetUserById/${userId}`, this.getHttpOptions());
}

  private getHttpOptions() {
    // Add any necessary headers or options here
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.getToken()}`
      })
    };
  }

  private getToken(): string {
    // Retrieve the token from local storage or any other method you use
    return localStorage.getItem('token') || '';
  }
}
