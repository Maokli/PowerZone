import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalConstants } from '../../Models/global-constants';
import { LoginModel } from '../../Models/loginModel';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { LocalCacheService } from 'src/app/Services/local-cache.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  url = GlobalConstants.apiURL + 'login';
  body = { username: '', password: '' };
  response: LoginModel;
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthenticationService,
    private cacheService: LocalCacheService
  ) {}

  ngOnInit(): void {}
  Login(): void {
    const headers = GlobalConstants.headers;
    this.authService.Login(this.body).subscribe((data) => {
      this.response = data;
      this.Redirect(this.response.role);
      this.authService.SetLoggedInUser(this.response);
      this.cacheService.CacheClient(this.response);
    });
  }

  Redirect(role: string): void {
    if (role === 'Manager') {
      this.router.navigate(['/dashboard']);
    }
  }

}
