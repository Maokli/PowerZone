import { MembershipModel } from './MembershipModel';

export class RegisterModel {
  username: string;

  name: string;

  familyName: string;

  dateOfBirth: Date;

  membershipTypeId: number;

  membershipType: MembershipModel;

  email: string;

  // optional
  phoneNumber: string;

  password: string;

  role: string;
}
