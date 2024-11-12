import { Component } from '@angular/core';
import { GateService } from '../../../services/gate.service';
import { Gate } from '../../../models/gate.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
@Component({
  selector: 'app-create-gate',
  templateUrl: './create-gate.component.html',
  styleUrls: ['./create-gate.component.css'],
})
export class CreateGateComponent {
  gateName: string = '';
  private baseUrl = `${environment.apiurl}/Gates`;

  constructor(private gateService: GateService) {}

  createGate() {
    // Call the service method and pass the gateName
    this.gateService.createGate(this.gateName).subscribe(
      (response) => {
        console.log('Gate created successfully:', response);
        alert('Gate Created ');
      },
      (error) => {
        console.error('Error creating gate:', error);
      }
    );
  }
}
