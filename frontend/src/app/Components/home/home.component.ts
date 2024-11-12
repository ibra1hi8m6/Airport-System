import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  activeComponent: string | null = null;
  activeSubComponent: string | null = null;

  toggleComponent(component: string) {
    if (this.activeComponent === component) {
      this.activeComponent = null; // Close the component if it's already open
    } else {
      this.activeComponent = component;
      this.activeSubComponent = null; // Reset subcomponent when a new main component is selected
    }
  }

  // Toggle the visibility of the subcomponent
  toggleSubComponent(subComponent: string) {
    if (this.activeSubComponent === subComponent) {
      this.activeSubComponent = null; // Close the subcomponent if it's already open
    } else {
      this.activeSubComponent = subComponent;
    }
  }
  showComponent(subComponent: string) {
    this.activeSubComponent = subComponent;
  }
}
