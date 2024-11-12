import { Component } from '@angular/core';
import { LoginFormModel } from '../../models/login';
import { AuthenticationService } from '../../services/authentication.service';
import { AuthService } from '../../services/auth.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginDto = new LoginFormModel();

  constructor(
    private authService: AuthenticationService,
    private authStatusService: AuthService,
    private cookieService: CookieService
  ) {}

  login(loginDto: LoginFormModel) {
    this.authService.login(loginDto).subscribe((jwtDto) => {

      this.cookieService.set('jwtToken', jwtDto.token, { path: '/' });
      localStorage.setItem('username', loginDto.Username);

      // Update the login status in AuthService
      this.authStatusService.login(loginDto.Username, loginDto.Password);
      console.log('Login successful');
    }, (error) => {
      console.error('Login failed', error);
    });
  }
}
