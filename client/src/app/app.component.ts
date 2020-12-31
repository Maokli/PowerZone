import { Component, OnInit } from '@angular/core';
import { GlobalConstants } from './Models/global-constants';
import { LocalCacheService } from './Services/local-cache.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private cacheService: LocalCacheService) {
    GlobalConstants.LoggedInClient = this.cacheService.GetCachedClient();
  }
}
