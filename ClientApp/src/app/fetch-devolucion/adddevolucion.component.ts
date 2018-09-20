import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchDevolucionApComponent } from '../fetch-devolucion/fetchdevolucionap.component';
import { DevolucionService } from '../services/devolservice.service';

@Component({
  selector: 'createdevol',
  templateUrl: './adddevolucion.component.html'
})

export class createDevolucionAp implements OnInit {

  devolForm: FormGroup;
  title: string = "Devolver";
  id: number;
  errorMessage: any;

  /*Se hace uso del mismo servicio, pero usando el metodo Editar, para utilizar 
   * el id de la garantia a devolver y registrarla, usando la tabla de G_Devolucion_det*/
  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _devolucionService: DevolucionService, private _router: Router) {
    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }

    this.devolForm = this._fb.group({
      id_devolucion: [''],
      id_det: [''],
      cheque: ['', [Validators.required]],
      fecha_dev: ['', [Validators.required]],
      cliente_recibe: ['', [Validators.required]],
      identificacion: ['', [Validators.required]],
      observacion: ['', [Validators.required]],
      usuario: [''],
      fechaReg: [''],
      fechaAct: ['']
    })
  }

  ngOnInit() {
    if (this.id > 0) {
      this.title = "Devolver";
      this._devolucionService.getDevolucionApById(this.id)
        .subscribe(resp => this.devolForm.setValue(resp)
          , error => this.errorMessage = error);
    }
  }

  save() {
    if (!this.devolForm.valid) {
      return;
    }

    if (this.title == "Registrar") {
      this._devolucionService.updateDevolucionAp(this.devolForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-devolucionap']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Devolver") {
      this._devolucionService.updateDevolucionAp(this.devolForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-devolucionap']);
        }, error => this.errorMessage = error)
    }
  }

  cancel() {
    this._router.navigate(['/fetch-devolucionap']);
  }

get id_devolucion() { return this.devolForm.get('id_devolucion'); }
get cheque() { return this.devolForm.get('cheque'); }
get fecha_dev() { return this.devolForm.get('fecha_dev'); }
get cliente_recibe() { return this.devolForm.get('cliente_recibe'); }
get identificacion() { return this.devolForm.get('identificacion'); }
get observacion() { return this.devolForm.get('observacion'); }
    
}
