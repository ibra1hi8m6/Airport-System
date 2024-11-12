// src/app/services/plane.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Plane, PlaneResponseModel } from '../models/plane.model';

@Injectable({
  providedIn: 'root'
})
export class PlaneService {
  private apiUrl = `${environment.apiurl}/Plane`;

  constructor(private http: HttpClient) {}

  getPlanes(): Observable<Plane[]> {
    return this.http.get<Plane[]>(`${this.apiUrl}/GetAllPlanes`);
  }

  getPlaneById(id: string): Observable<Plane> {
    return this.http.get<Plane>(`${this.apiUrl}/GetPlaneById/${id}`);
  }

  createPlane(planeForm: Plane): Observable<PlaneResponseModel> {
    return this.http.post<PlaneResponseModel>(`${this.apiUrl}/CreatePlane`, planeForm);
  }

  updatePlane(id: string, planeForm: Plane): Observable<PlaneResponseModel> {
    return this.http.put<PlaneResponseModel>(`${this.apiUrl}/UpdatePlane/${id}`, planeForm);
  }

  deletePlane(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeletePlane/${id}`);
  }

  getPlanesByPayload(payload: number): Observable<Plane[]> {
    return this.http.get<Plane[]>(`${this.apiUrl}/GetPlanesByPayload?payload=${payload}`);
  }

  getPlanesBySeats(seats: number): Observable<Plane[]> {
    return this.http.get<Plane[]>(`${this.apiUrl}/GetPlanesBySeats?seats=${seats}`);
  }

  updatePlaneWithEvenPayload(id: string, planeForm: Plane): Observable<Plane> {
    return this.http.put<Plane>(`${this.apiUrl}/UpdateEvenPayload/${id}`, planeForm);
  }
}
