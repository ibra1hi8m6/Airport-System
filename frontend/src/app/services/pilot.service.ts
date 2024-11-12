import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class PilotService {
  private apiUrl = `${environment.apiurl}/UserManagement/GetUsersByRole`; // Adjust this URL as per your API

  constructor(private http: HttpClient) {}

  getPilots(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
  getPilotsByRole(role: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${role}`);
  }
}
