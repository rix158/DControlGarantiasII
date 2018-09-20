import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ClienteBlService } from '../services/cliblservice.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Component({
    selector: 'fetchclientebl',
    templateUrl: './fetchclientebl.component.html'
})

export class FetchClienteBlComponent {

  clientsbl: any[];
  dataTable: any;
  public clieblList: ClienteBlData[];
        
  constructor(public http: Http, private _router: Router, private _clienteblService: ClienteBlService, private chRef: ChangeDetectorRef) {
        this.getClientesBl();
    }

    getClientesBl() {
      this._clienteblService.getClientesBl()
        .subscribe((data: any[]) => {
          this.clientsbl = data;

          this.chRef.detectChanges();
          // Now you can use jQuery DataTables :
          const table: any = $('table');
          this.dataTable = table.DataTable();
        });
    }

    delete(clienteBlId) {
        var ans = confirm("Está seguro que desea eliminar el cliente seleccionado?");
        if (ans) {
            this._clienteblService.deleteClienteBl(clienteBlId).subscribe((data) => {
                this.getClientesBl();
            }, error => console.error(error))
        }
    }
}

interface ClienteBlData {
    id: number;
    ruc: string;
    cliente: string;
    cod_bl: string;
    exoneracion: string;
    observacion: string;
}
