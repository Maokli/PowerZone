import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalConstants } from 'src/app/Models/global-constants';
import { LoginModel } from 'src/app/Models/loginModel';
import { MembershipModel } from 'src/app/Models/MembershipModel';
import { RegisterModel } from 'src/app/Models/registerModel';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { LocalCacheService } from 'src/app/Services/local-cache.service';
import { ManagerService } from 'src/app/Services/manager.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  url = GlobalConstants.apiURL + 'signup';
  body: RegisterModel = new RegisterModel();
  response: LoginModel;
  memberships: MembershipModel[];
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthenticationService,
    private cacheService: LocalCacheService,
    private managerService: ManagerService
  ) {}

  ngOnInit(): void {
    this.GetMemberships();
  }
  Register(): void {
    console.log(this.body);
    this.authService.Register(this.body).subscribe((data) => {
      this.response = data;
      this.authService.SetLoggedInUser(this.response);
      this.cacheService.CacheClient(this.response);
    });
  }

  GetMemberships(): any{
    this.managerService.GetMemberships().subscribe(data => {
      this.memberships = data;
    });
  }
}
