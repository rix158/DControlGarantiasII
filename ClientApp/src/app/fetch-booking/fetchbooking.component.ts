import { Component, Inject, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { BookingService } from '../services/bookservice.service';
import { Table } from 'primeng/table';

@Component({
  selector: 'fetchbooking',
  templateUrl: './fetchbooking.component.html'
})

export class FetchBookingComponent {
  public bookList: Book[];
  cols: any[];
  booking_envio;
  modo_envio;

  constructor(public http: Http, private _router: Router, private _bookingService: BookingService) {
    this.cols = [
      { field: 'ids_bl', header: 'ID' },
      { field: 'cod_bl', header: 'Booking' },
      { field: 'ciu_origen', header: 'Ciudad de Origen' },
      { field: 'ciu_destino', header: 'Ciudad de Destino' },
      { field: 'pto_origen', header: 'Puerto de Origen' },
      { field: 'pto_destino', header: 'Puerto de Destino' },
      { field: 'cod_linea', header: 'Linea' },
      { field: 'fec_embarque', header: 'Fecha de Embarque' }
    ];
    this.getBooking();
  }


  getBooking() {
    this._bookingService.getBooking().subscribe(
      data => {
        this.bookList = data;
        console.log(this.bookList[0]);
      }
    )
  }
  getDetalle(booking) {

  }
  /**
   * Metodo para procesar enviar por ftp un booking de acuerdo al estado
  */
  procesar() {
    console.log(this.booking_envio + "--" + this.modo_envio);

  }
  seleccionar(booking, tipo) {
    this.booking_envio = booking;
    this.modo_envio = tipo;
  }
}

interface Book {
  ids_bl: number,
  cod_bl: string,
  ciu_origen: string,
  ciu_destino: string,
  pto_origen: string,
  pto_destino: string,
  cod_linea: string,
  fecha_embarque: string,
  /* Nuevo campo concatenado que contiene todos los detalles del booking*/
  bookingDetalleList: BookingDetData[]
}

interface BookingDetData {
  ids_bl: number,
  cod_carga: string,
  cod_peligro: string,
  cod_tipcont: string,
  cod_container: string,
  val_peso: number,
  des_sello1: string,
  des_sello2: string,
  des_sello3: string,
  des_sello4: string,
}
