import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingComponent } from './pages/landing/landing.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SharedModule } from './shared/shared.module';
import { BarChartComponent } from './pages/dashboard/components/bar-chart/bar-chart.component';
import { LineChartComponent } from './pages/dashboard/components/line-chart/line-chart.component';
import { SidebarComponent } from './pages/dashboard/components/sidebar/sidebar.component';
import { LoginComponent } from './pages/login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './pages/register/register.component';
import { StorageServiceModule } from 'ngx-webstorage-service';

@NgModule({
  declarations: [
    AppComponent,
    BarChartComponent,
    LineChartComponent,
    SidebarComponent,
    DashboardComponent,
    LandingComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    FormsModule,
    HttpClientModule,
    StorageServiceModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
