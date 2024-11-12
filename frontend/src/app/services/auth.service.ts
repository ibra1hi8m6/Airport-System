import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInStatus = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedInStatus.asObservable();
  }

  getUsername(): string {
    return localStorage.getItem('username') || '';  // Or fetch it from your backend
  }

  login(username: string, token: string): void {
    localStorage.setItem('username', username);
    localStorage.setItem('jwtToken', token); // Store the token
    this.loggedInStatus.next(true);
  }

  logout(): void {
    localStorage.removeItem('username');
    localStorage.removeItem('jwtToken');
    this.loggedInStatus.next(false);
  }
  initializeAuthStatus(): void {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      this.loggedInStatus.next(true);
    } else {
      this.loggedInStatus.next(false);
    }
  }
}
