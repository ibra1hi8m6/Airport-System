import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable,BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginFormModel} from '../models/login';
import { SignUpFormModel} from '../models/register';
import { JwtAuth } from '../models/jwtAuth';
@Injectable({
  providedIn: 'root'
})
export class UserManagerService {
  private loggedInStatus = new BehaviorSubject<boolean>(false);
registerUrl = "UserManagement/SignUpNewUser"
loginUrl = "UserManagement/LoginUser"
private apiUrl = `${environment.apiurl}/UserManagement`;
  constructor(
    private http: HttpClient
  ) { }

  public register(user: SignUpFormModel): Observable<JwtAuth>{
    return this.http.post<JwtAuth>( `${environment.apiurl}/${this.registerUrl}`,user);
  }
  public login(user: LoginFormModel): Observable<JwtAuth>{
    return this.http.post<JwtAuth>( `${environment.apiurl}/${this.loginUrl}`,user);
  }

  getAllRoles(): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetAllRoles`);
  }
  getUserById(id: string): Observable<SignUpFormModel> {
    return this.http.get<SignUpFormModel>(`${this.apiUrl}/GetUserById/${id}`);
  }
  getUsersByRole(role: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetUsersByRole/${role}`);
  }

  updateUser(id: string, updatedUser: SignUpFormModel): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUser/${id}`, updatedUser);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteUser/${id}`);
  }
}
