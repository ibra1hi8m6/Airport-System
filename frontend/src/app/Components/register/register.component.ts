import { Component } from '@angular/core';
import { SignUpFormModel } from '../../models/register';
import { AuthenticationService } from '../../services/authentication.service';
import { UserManagerService } from '../../services/user-manager.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'] // Corrected to styleUrls
})
export class RegisterComponent {
  registerDto = new SignUpFormModel();
  roles: any[] = [];

  constructor(private authService: AuthenticationService,
    private userManagerService: UserManagerService
  ) {}
  ngOnInit() {
    this.userManagerService.getAllRoles().subscribe((data) => {
      this.roles = data;
    });
  }

  register(registerDto: SignUpFormModel) {
    const roleValue = parseInt(this.registerDto.Role.toString(), 10);
    const payload = {
      City: this.registerDto.City,
      ConfirmPassword: this.registerDto.ConfirmPassword,
      Country: this.registerDto.Country,
      DateOfBirth: this.registerDto.DateOfBirth,
      DoctorCode: this.registerDto.DoctorCode,
      Email: this.registerDto.Email,
      FirstName: this.registerDto.FirstName,
      HouseNumber: this.registerDto.HouseNumber,
      LastName: this.registerDto.LastName,
      Password: this.registerDto.Password,
      PhoneNumber: this.registerDto.PhoneNumber,
      Role: roleValue,
      Street: this.registerDto.Street,
      TotalHours: this.registerDto.TotalHours,
      Username: this.registerDto.Username,
    };

    this.authService.register(payload).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
      },
      error: (error) => {
        console.error('Registration failed', error);
      }
    });
  }
}
