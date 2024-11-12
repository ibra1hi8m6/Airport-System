import { Component, OnInit } from '@angular/core';
import { PlaneService } from '../../../services/plane.service';
import { Plane } from '../../../models/plane.model';

@Component({
  selector: 'app-get-all-planes',
  templateUrl: './get-all-planes.component.html',
  styleUrls: ['./get-all-planes.component.css']
})
export class GetAllPlanesComponent implements OnInit {
  planes: Plane[] = [];
  message: string = '';

  constructor(private planeService: PlaneService) {}

  ngOnInit() {
    // Initialization logic if needed
  }

  getPlanes() {
    this.planeService.getPlanes().subscribe(
      (planes: Plane[]) => {
        this.planes = planes;
      },
      error => {
        this.message = 'Error loading planes: ' + error.message;
      }
    );
  }
}
