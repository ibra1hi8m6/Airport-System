import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { LoginFormModel} from '../models/login';
import { SignUpFormModel} from '../models/register';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import { JwtAuth } from '../models/jwtAuth';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
registerUrl = "UserManagement/SignUpNewUser"
loginUrl = "UserManagement/LoginUser"
  constructor(private http: HttpClient) { }

  public register(user: SignUpFormModel): Observable<JwtAuth>{
    return this.http.post<JwtAuth>( `${environment.apiurl}/${this.registerUrl}`,user);
  }
  public login(user: LoginFormModel): Observable<JwtAuth>{
    return this.http.post<JwtAuth>( `${environment.apiurl}/${this.loginUrl}`,user);
  }
}
