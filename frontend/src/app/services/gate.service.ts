import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gate } from '../models/gate.model';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root',
})
export class GateService {
  private baseUrl = `${environment.apiurl}/Gates`;  // Replace with your actual backend URL

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwtToken'); // Ensure this matches the key you use to store the token
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  createGate(name: string): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.post<any>(`${this.baseUrl}/CreateGate`, { name }, { headers });
  }

  deleteGate(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteGate/${id}`);
  }

  updateGate(id: string, gate: Gate): Observable<Gate> {
    return this.http.put<Gate>(`${this.baseUrl}/UpdateGate/${id}`, gate);
  }

  getAllGates(): Observable<Gate[]> {
    return this.http.get<Gate[]>(`${this.baseUrl}/GetAllGates`);
  }

  getGateById(id: string): Observable<Gate> {
    return this.http.get<Gate>(`${this.baseUrl}/GetGateById/${id}`);
  }

  getGatesWithPagination(page: number, pageSize: number = 5): Observable<{ gates: Gate[], totalPages: number }> {
    let params = new HttpParams().set('page', page.toString()).set('pageSize', pageSize.toString());
    return this.http.get<{ gates: Gate[], totalPages: number }>(`${this.baseUrl}/page/${page}`, { params });
  }
}
