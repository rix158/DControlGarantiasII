import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { GarantiaService } from '../services/garanservice.service';
import { saveAs } from 'file-saver/FileSaver';
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
    let data = [
      { cod_consignatario: 'hola', fecha: 'fecha' }
    ];
    const replacer = (key, value) => value === null ? '' : value; // specify how you want to handle null values here
    const header = Object.keys(data[0]);
    let csv = data.map(row => header.map(fieldName => JSON.stringify(row[fieldName], replacer)).join(','));
    csv.unshift(header.join(','));
    let csvArray = csv.join('\r\n');

    var a = document.createElement('a');
    var blob = new Blob([csvArray], { type: 'text/csv' }),
      url = window.URL.createObjectURL(blob);

    a.href = url;
    a.download = "myFile.csv";
    a.click();
    window.URL.revokeObjectURL(url);
    a.remove();
  }
}
