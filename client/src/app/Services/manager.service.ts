import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GlobalConstants } from '../Models/global-constants';
import { MembershipModel } from '../Models/MembershipModel';
import { RegisterModel } from '../Models/registerModel';

@Injectable({
  providedIn: 'root',
})
export class ManagerService {
  constructor(private http: HttpClient) {}

  GetClients(headers): Observable<RegisterModel[]> {
    return this.http.get<RegisterModel[]>(GlobalConstants.apiURL + 'manager', {
      headers,
    });
  }
  GetMemberships(): Observable<MembershipModel[]> {
    return this.http.get<MembershipModel[]>(
      GlobalConstants.apiURL + 'membership'
    );
  }
  DeleteMembership(headers, id): Observable<any> {
    return this.http.delete<MembershipModel[]>(
      GlobalConstants.apiURL + 'manager/membership/' + id,
      {headers}
    );
  }
  AddMembership(headers, name, price): Observable<any> {
    const newMembership: MembershipModel = {
      name,
      price,
    };
    return this.http.post<MembershipModel[]>(
      GlobalConstants.apiURL + 'manager/membership/add',
      newMembership,
      {headers}
    );
  }
  UpdateMembership(headers, id: number, name: string, price: number): Observable<any> {
    const newMembership: MembershipModel = {
      name,
      price,
    };
    return this.http.put<MembershipModel[]>(
      GlobalConstants.apiURL + 'manager/membership/update/' + id,
      newMembership,
      {headers}
    );
  }
}
