import { Component, OnInit } from '@angular/core';
import { GateService } from '../../../services/gate.service';
import { Gate } from '../../../models/gate.model';

@Component({
  selector: 'app-delete-gate',
  templateUrl: './delete-gate.component.html',
  styleUrls: ['./delete-gate.component.css']
})
export class DeleteGateComponent implements OnInit {
  gateId: string = '';
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

  deleteGate(): void {
    if (this.gateId) {
      this.gateService.deleteGate(this.gateId).subscribe(() => {
        console.log('Gate deleted');
        this.fetchGates();  // Refresh the gate list after deletion
        this.gateId = '';   // Optionally reset the selected gate ID
      }, error => {
        console.error('Error deleting gate:', error);
      });
    }
  }
}
