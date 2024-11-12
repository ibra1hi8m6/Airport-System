import { Component, OnInit } from '@angular/core';
import { PlaneService } from '../../../services/plane.service';
import { Plane } from '../../../models/plane.model';

@Component({
  selector: 'app-delete-plane',
  templateUrl: './delete-plane.component.html',
  styleUrls: ['./delete-plane.component.css'],
})
export class DeletePlaneComponent implements OnInit {
  planes: Plane[] = [];
  selectedPlaneId: string = '';

  constructor(private planeService: PlaneService) {}

  ngOnInit(): void {
    this.getAllPlanes();
  }

  getAllPlanes(): void {
    this.planeService.getPlanes().subscribe((data: Plane[]) => {
      this.planes = data;
    });
  }

  deletePlane(): void {
    if (this.selectedPlaneId) {
      this.planeService.deletePlane(this.selectedPlaneId).subscribe(() => {
        this.getAllPlanes(); // Refresh the list after deletion
      });
    }
  }

  onPlaneSelected(event: any): void {
    this.selectedPlaneId = event.target.value;
  }
}
