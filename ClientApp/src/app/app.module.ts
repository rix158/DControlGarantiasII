import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ngCsv } from 'ng-csv';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { FetchClienteComponent } from './fetch-cliente/fetchcliente.component';
import { createcliente } from './fetch-cliente/addcliente.component';
import { FetchClienteBlComponent } from './fetch-clientebl/fetchclientebl.component';
import { createclientebl } from './fetch-clientebl/addclientebl.component';
import { FetchDevolucionComponent } from './fetch-devolucion/fetchdevolucion.component';
import { createDevolucion } from './fetch-devolucion/addsoldevolucion.component';
import { createDevolucionAp } from './fetch-devolucion/adddevolucion.component';
import { FetchDevolucionApComponent } from './fetch-devolucion/fetchdevolucionap.component';
import { createGarantia } from './fetch-garantia/addgarantia.component';
import { FetchGarantiaComponent } from './fetch-garantia/fetchgarantia.component';
import { reporteGarantia } from './fetch-garantia/reporte.component';
import { FetchCierreIComponent } from './fetch-cierre/fetchcierrei.component';
import { FetchCierreEComponent } from './fetch-cierre/fetchcierree.component';
import { FetchBookingComponent } from './fetch-booking/fetchbooking.component';
import { LoginComponent } from './login/login.component';

import { ClienteService } from './services/cliservice.service';
import { ClienteBlService } from './services/cliblservice.service';
import { GarantiaService } from './services/garanservice.service';
import { DevolucionService } from './services/devolservice.service';
import { CierreService } from './services/cierreservice.service';
import { BookingService } from './services/bookservice.service';
import { LoginService } from './services/login.service';

import { TableModule } from 'primeng/table';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchClienteComponent,
    createcliente,
    FetchClienteBlComponent,
    createclientebl,
    createDevolucion,
    createDevolucionAp,
    FetchDevolucionComponent,
    createGarantia,
    FetchGarantiaComponent,
    reporteGarantia,
    FetchCierreIComponent,
    FetchCierreEComponent,
    FetchDevolucionApComponent,
    LoginComponent,
    FetchBookingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,    
    ReactiveFormsModule,
    TypeaheadModule.forRoot(),
    TooltipModule.forRoot(),
    RouterModule.forRoot([
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent },
      
      { path: 'fetch-cliente', component: FetchClienteComponent },
      { path: 'register-cliente', component: createcliente },
      { path: 'cliente/edit/:id', component: createcliente },

      { path: 'fetch-clientebl', component: FetchClienteBlComponent },
      { path: 'register-clientebl', component: createclientebl },
      { path: 'clientebl/edit/:id', component: createclientebl },

      { path: 'fetch-garantia', component: FetchGarantiaComponent },
      { path: 'register-garantia', component: createGarantia },
      { path: 'garantia/edit/:id', component: createGarantia },
      { path: 'garantia/reporte/:id', component: reporteGarantia },

      { path: 'fetch-devolucion', component: FetchDevolucionComponent },
      { path: 'fetch-devolucionap', component: FetchDevolucionApComponent },
      { path: 'register-devolucion', component: createDevolucion },
      { path: 'devolucionap/edit/:id', component: createDevolucionAp },

      { path: 'fetch-pagos', component: FetchCierreIComponent },
      { path: 'fetch-devoluciones', component: FetchCierreEComponent },
      { path: 'fetch-booking', component: FetchBookingComponent },
      { path: '**', redirectTo: 'home' }
    ]), TableModule
  ],
  providers: [ClienteService, ClienteBlService, DevolucionService, GarantiaService, CierreService, BookingService, LoginService],
  bootstrap: [AppComponent, NavMenuComponent]
})
export class AppModule { }
