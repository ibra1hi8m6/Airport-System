import { Component, OnInit } from '@angular/core';
import { GateService } from '../../../services/gate.service';
import { Gate } from '../../../models/gate.model';

@Component({
  selector: 'app-update-gate',
  templateUrl: './update-gate.component.html',
  styleUrls: ['./update-gate.component.css']
})
export class UpdateGateComponent implements OnInit {
  gate: Gate = { id: '', name: '' };
  gates: Gate[] = [];

  constructor(private gateService: GateService) {}

  ngOnInit(): void {
    this.fetchGates();
  }

  fetchGates(): void {
    this.gateService.getAllGates().subscribe(response => {
      this.gates = response;
    }, error => {
      console.error('Error fetching gates:', error);
    });
  }

  updateGate(): void {
    if (this.gate.id) {
      this.gateService.updateGate(this.gate.id, this.gate).subscribe(response => {
        console.log('Gate updated:', response);
        // Handle success
      }, error => {
        console.error('Error updating gate:', error);
        // Handle error
      });
    }
  }
}
