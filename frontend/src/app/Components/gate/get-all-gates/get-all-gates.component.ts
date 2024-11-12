import { Component } from '@angular/core';
import { GateService } from '../../../services/gate.service';
import { Gate } from '../../../models/gate.model';

@Component({
  selector: 'app-get-all-gates',
  templateUrl: './get-all-gates.component.html',
  styleUrls: ['./get-all-gates.component.css']
})
export class GetAllGatesComponent {
  gates: Gate[] = [];

  constructor(private gateService: GateService) {}

  getGates(): void {
    this.gateService.getAllGates().subscribe(response => {
      this.gates = response;
    }, error => {
      console.error('Error fetching gates:', error);
    });
  }
}
