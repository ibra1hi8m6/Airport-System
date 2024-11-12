import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../src/app/services/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // Corrected to styleUrls
})
export class AppComponent implements OnInit {
  isLoggedIn: boolean = false;
  username: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.initializeAuthStatus();
    this.authService.isLoggedIn.subscribe(status => {
      this.isLoggedIn = status;
      this.username = this.authService.getUsername(); // Update username based on AuthService
    });
  }

  logout(): void {
    this.authService.logout();
  }
}
