import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ClienteService } from '../services/cliservice.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Component({
  selector: 'fetchcliente',
  templateUrl: './fetchcliente.component.html'
})

export class FetchClienteComponent {

  clients: any[];
  dataTable: any;
  public clieList: ClienteData[];

  constructor(public http: Http, private _router: Router, private _clienteService: ClienteService, private chRef: ChangeDetectorRef) {
    this.getClientes();
  }

  getClientes() {
    this._clienteService.getClientes()
      .subscribe((data: any[]) => {
        this.clients = data;
        
    this.chRef.detectChanges();
    const table: any = $('table');
    this.dataTable = table.DataTable();
  });
}

  delete(clienteId) {
    var ans = confirm("Está seguro que desea eliminar al cliente seleccionado?");
    if (ans) {
      this._clienteService.deleteCliente(clienteId).subscribe((data) => {
        this.getClientes();
      }, error => console.error(error))
    }
  }
}

interface ClienteData {
    id: number;
    ruc: string;
    cliente: string;
    exoneracion: string;
    fechaReg: string;
    observacion: string;
}
