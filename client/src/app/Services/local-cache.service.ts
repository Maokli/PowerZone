import { Inject, Injectable } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';
import { LoginModel } from '../Models/loginModel';

@Injectable({
  providedIn: 'root',
})
export class LocalCacheService {
  KEY = 'local_loggedInClient';

  constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) {}

  CacheClient(loggedClient: LoginModel): void {
    let currentClient = this.storage.get(this.KEY) || null;
    if (currentClient === loggedClient) {
      console.log('Already Logged In');
      return;
    }
    currentClient = loggedClient;
    this.storage.set(this.KEY, currentClient);
    console.log(this.storage.get(this.KEY));
  }

  ClearLogin(): void {
    this.storage.remove(this.KEY);
  }

  GetCachedClient(): LoginModel {
    const currentClient = this.storage.get(this.KEY) || null;
    if (currentClient) {
      return currentClient;
    }
    return new LoginModel();
  }
}
