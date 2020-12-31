import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from 'src/app/Models/global-constants';
import { MembershipModel } from 'src/app/Models/MembershipModel';
import { RegisterModel } from 'src/app/Models/registerModel';
import { ManagerService } from 'src/app/Services/manager.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  date = new Date().getFullYear();
  totalClients = 0;
  Clients: RegisterModel[];
  Memberships: MembershipModel[];
  MonthlyIncome = 0;
  YearlyIncome = 0;
  headers = {
    Authorization: 'Bearer ' + GlobalConstants.LoggedInClient.token,
    'Content-Type': 'Application/json',
  };
  constructor(private managerService: ManagerService) {}

  ngOnInit(): void {
    this.GetClients();
    this.GetMemberships();
  }

  GetClients(): void {
    this.managerService
      .GetClients(this.headers)
      .subscribe((response: RegisterModel[]) => {
        console.log(response);
        this.totalClients = response.length;
        this.Clients = response;
      });
  }
  GetMemberships(): void {
    this.managerService
      .GetMemberships()
      .subscribe((response: MembershipModel[]) => {
        this.Memberships = response;
        this.CalculateIncome();
      });
  }
  DeleteMembership(id): void {
    this.managerService.DeleteMembership(this.headers, id).subscribe(() => {
      this.GetMemberships();
    });
  }
  AddMembership(name, price): void {
    this.managerService
      .AddMembership(this.headers, name, price)
      .subscribe((response) => {
        console.log(response);
        this.GetMemberships();
      });
  }
  UpdateMembership(id, name, price): void {
    this.managerService
      .UpdateMembership(this.headers, id, name, price)
      .subscribe(() => {
        this.GetMemberships();
      });
  }
  CalculateIncome(): void {
    this.Clients.forEach((client) => {
      if (client.membershipType.name === 'Monthly') {
        this.MonthlyIncome += client.membershipType.price;
        this.YearlyIncome += client.membershipType.price * 12;
      }
      if (client.membershipType.name === 'Yearly') {
        this.MonthlyIncome += client.membershipType.price / 12;
        this.YearlyIncome += client.membershipType.price;
      }
    });
  }
}
