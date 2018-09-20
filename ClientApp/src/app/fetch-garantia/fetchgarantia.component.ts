import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { GarantiaService } from '../services/garanservice.service';
//import { Angular5Csv } from 'angular5-csv/Angular5-csv';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Component({
  selector: 'fetchgarantia',
  templateUrl: './fetchgarantia.component.html'
})

export class FetchGarantiaComponent {

  garants: any[];
  dataTableG: any;

  constructor(public http: Http, private _router: Router, private _garantiaService: GarantiaService, private chRef: ChangeDetectorRef) {
    this.getGarantias();
  }

  getGarantias() {
    this._garantiaService.getGarantias()
      .subscribe((data: any[]) => {
        this.garants = data;
        this.chRef.detectChanges();
        const table: any = $('table').DataTable();
        this.dataTableG = table.DataTable();
      });
  }

  delete(garantiaId) {
    var ans = confirm("Está seguro que desea eliminar el registro seleccionado?");
    if (ans) {
      this._garantiaService.deleteGarantia(garantiaId).subscribe((data) => {
        this.getGarantias();
      }, error => console.error(error))
    }
  }

  generarCobro(garantia) {
    //alert('valimos');
    //let data = [
    //  { cod_consignatario: garantia.cod_consignatario, fecha: garantia.fecha_registro }
    //];
    //let options = {
    //  fieldSeparator: ',',
    //  quoteStrings: '"',
    //  decimalseparator: '.',
    //  showLabels: true,
    //  showTitle: true,
    //  title: 'Prueba del formato de carga',
    //  noDownload: false,
    //  headers: ["First Name", "Last Name", "ID"]
    //};

    //new Angular5Csv(data, "C:\FormatoCarga.csv", options);
  }
}
