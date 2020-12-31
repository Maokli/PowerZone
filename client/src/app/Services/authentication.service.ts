import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GlobalConstants } from '../Models/global-constants';
import { LoginModel } from '../Models/loginModel';
import { RegisterModel } from '../Models/registerModel';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private http: HttpClient) {}

  Login(body): Observable<LoginModel> {
    const headers = GlobalConstants.headers;
    return this.http.post<LoginModel>(GlobalConstants.apiURL + 'login', body, {
      headers,
    });
  }

  Register(body): Observable<LoginModel> {
    const headers = GlobalConstants.headers;
    return this.http.post<LoginModel>(GlobalConstants.apiURL + 'signup', body, {
      headers,
    });
  }

  SetLoggedInUser(user: LoginModel): void {
    if (user) {
      GlobalConstants.LoggedInClient = user;
    }
  }
}
