import { Component, OnInit } from '@angular/core';
import { UserManagerService } from '../../../services/user-manager.service';
import { SignUpFormModel } from '../../../models/register';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {
  roles: any[] = [];
  users: any[] = [];
  selectedRole: number | null = null;
  selectedUserId: string | null = null;
  selectedUser: any;
  //updateForm:  SignUpFormModel = new SignUpFormModel();
  updateForm: FormGroup | undefined;

  constructor(
    private userManagerService: UserManagerService,
    private fb: FormBuilder
  ) {
    this.updateForm = this.fb.group({
      id: [null],
      username: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [''],  // Password fields can be optional in the update form
      confirmPassword: [''],
      role: [0, Validators.required],
      phoneNumber: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      totalHours: [0],
      doctorCode: [''],
      houseNumber: [null],
      street: [''],
      city: [''],
      country: ['']
    });

  }

  ngOnInit(): void {

    this.userManagerService.getAllRoles().subscribe((data) => {
      this.roles = data;
    });
  }

  onRoleChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const role = parseInt(selectElement.value, 10);

    this.selectedRole = role;
    this.userManagerService.getUsersByRole(role).subscribe((users) => {
      this.users = users;
    });
  }

  onUserSelect(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedUserId = selectElement.value;

    if (this.selectedUserId) {
      this.userManagerService.getUserById(this.selectedUserId).subscribe((user) => {
        this.selectedUser = user;
        this.updateForm!.patchValue({  // Use the non-null assertion operator here
          id: user.id,
          username: user.Username,
          firstName: user.FirstName,
          lastName: user.LastName,
          email: user.Email,
          phoneNumber: user.PhoneNumber,
          dateOfBirth: user.DateOfBirth,
          totalHours: user.TotalHours,
          doctorCode: user.DoctorCode,
          houseNumber: user.HouseNumber,
          street: user.Street,
          city: user.City,
          country: user.Country,  // Populate the model with fetched user data
        });
      });
    }else {
      // Handle the case where no user is selected
      this.updateForm!.reset();
    }
  }



  updateUser() {
    if (this.selectedUserId && this.updateForm) {
        const updatedUser: SignUpFormModel = this.updateForm.value as SignUpFormModel;

        this.userManagerService.updateUser(this.selectedUserId, updatedUser).subscribe((response) => {
            // Handle success response
            console.log('User updated successfully');
        }, (error) => {
            // Handle error response
            console.error('Error updating user', error);
        });
    }
}

}
