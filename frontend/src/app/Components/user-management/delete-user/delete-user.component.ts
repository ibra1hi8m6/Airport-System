import { Component, OnInit } from '@angular/core';
import { UserManagerService } from '../../../services/user-manager.service';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {
  roles: any[] = [];
  users: any[] = [];
  selectedRole: number | null = null;
  selectedUserId: string | null = null;

  constructor(private userManagerService: UserManagerService) { }

  ngOnInit(): void {
    this.userManagerService.getAllRoles().subscribe((data) => {
      this.roles = data;
    });
  }

  onRoleChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const role = Number(selectElement.value);
    this.selectedRole = role;
    this.userManagerService.getUsersByRole(role).subscribe((users) => {
      this.users = users;
    });
  }

  onUserSelect(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedUserId = selectElement.value;
  }

  deleteUser() {
    if (this.selectedUserId) {
      this.userManagerService.deleteUser(this.selectedUserId).subscribe(() => {
        console.log('User deleted successfully');
        // Refresh the user list or provide a success message
        this.users = this.users.filter(user => user.id !== this.selectedUserId);
        this.selectedUserId = null;
      }, (error) => {
        console.error('Error deleting user', error);
      });
    }
  }
}
