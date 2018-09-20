import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { DevolucionService } from '../services/devolservice.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Component({
  selector: 'fetchdevolucion',
  templateUrl: './fetchdevolucion.component.html'
})

export class FetchDevolucionComponent {

  devols: any[];
  datatabledev: any;
  public devList: DevolucionData[];

  constructor(public http: Http, private _router: Router, private _devolucionService: DevolucionService, private chRef: ChangeDetectorRef) {
    this.getDevoluciones();
  }

  getDevoluciones() {
    this._devolucionService.getDevoluciones()
      .subscribe((data: any[]) => {
        this.devols = data;

        this.chRef.detectChanges();
        const table: any = $('table');
        this.datatabledev = table.DataTable();
      });
  }

  /*Metodo que comunica la solicitud ha sido aprobada -> bdd*/
  aprobar(id_devolucion) {
    this._devolucionService.updateDevolucion(id_devolucion)
      .subscribe((data) => {
      this.getDevoluciones();
    }, error => console.error(error))
  }
}

interface DevolucionData {
  id_devolucion: number;
  fecha_registro: string;
  cliente: string;
  consignatario: string;
  doc_recibo_cheque: string;
  doc_EIR: string;
  estado_apr: string;
  cheque: string;
  usuario: string;
  fechaReg: string;
  fechaAct: string;
}
