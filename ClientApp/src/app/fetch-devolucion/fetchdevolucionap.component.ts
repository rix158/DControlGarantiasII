import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { DevolucionService } from '../services/devolservice.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Component({
  selector: 'fetchdevolucionap',
  templateUrl: './fetchdevolucionap.component.html'
})

export class FetchDevolucionApComponent {

  devolsap: any[];
  datatable: any;
  public devList: DevolucionData[];

  constructor(public http: Http, private _router: Router, private _devolucionService: DevolucionService, private chRef: ChangeDetectorRef) {
    this.getDevolucionesAp();
  }

  getDevolucionesAp() {
    this._devolucionService.getDevolucionesAp()
      .subscribe((data: any[]) => {
        this.devolsap = data;

        this.chRef.detectChanges();
        const table: any = $('table');
        this.datatable = table.DataTable();
      });
  }
}

interface DevolucionData {
  id_devolucion: number;
  fecha_dev: string;
  cliente: string;
  consignatario: string;
  doc_recibo_cheque: string;
  doc_EIR: string;
  estado_apr: string;
  usuario: string;
  fechaReg: string;
  fechaAct: string;
}
