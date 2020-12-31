import { LoginModel } from './loginModel';

export class GlobalConstants {
  public static apiURL  = 'https://localhost:5001/api/';
  public static headers: any = {  'Content-Type': 'Application/json' };
  public static LoggedInClient: LoginModel;

}
