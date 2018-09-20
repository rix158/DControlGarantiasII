import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchDevolucionComponent } from '../fetch-devolucion/fetchdevolucion.component';
import { GarantiaService } from '../services/garanservice.service';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';


@Component({
  selector: 'reporte',
  templateUrl: './reporte.component.html',
  styleUrls: ['./reporte.component.css']
})

export class reporteGarantia implements OnInit {


  public gar: any;
  garantiaForm: FormGroup;
  title: string = "reporte";
  id: number;
  errorMessage: any;

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _garantiaService: GarantiaService, private _router: Router) {
    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }

    this.garantiaForm = this._fb.group({
      id_garantia: 0,
      cod_bl: ['', [Validators.required]],
      nave: ['', [Validators.required]],
      cliente: ['', [Validators.required]],
      banco: ['', [Validators.required]],
      numero_cuenta: ['', [Validators.required]],
      consignatario: ['', [Validators.required]],
      contenedores: ['', [Validators.required]],
      cod_container: ['', [Validators.required]],
      tipo_contenedor: ['', [Validators.required]],
      valor: ['', [Validators.required]],
      cheque: ['', [Validators.required]],
      tipo_pago: ['', [Validators.required]],
      fecha_registro: [''],
      usuario: [''],
      fechaReg: [''],
      fechaAct: ['']
    })
  }

  ngOnInit() {
    if (this.id > 0) {
      this._garantiaService.getGarantiaById(this.id)
        .subscribe(data => {
          this.gar = data;
        }, error => this.errorMessage = error)
    }
  }

  cancel() {
    this._router.navigate(['/fetch-garantia']);
  }

  get cod_bl() { return this.garantiaForm.get('cod_bl'); }
  get nave() { return this.garantiaForm.get('nave'); }
  get cliente() { return this.garantiaForm.get('cliente'); }
  get banco() { return this.garantiaForm.get('banco'); }
  get numero_cuenta() { return this.garantiaForm.get('numero_cuenta'); }
  get consignatario() { return this.garantiaForm.get('consignatario'); }
  get contenedores() { return this.garantiaForm.get('contenedores'); }
  get cod_container() { return this.garantiaForm.get('cod_container'); }
  get tipo_contenedor() { return this.garantiaForm.get('tipo_contenedor'); }
  get valor() { return this.garantiaForm.get('valor'); }
  get cheque() { return this.garantiaForm.get('cheque'); }
  get tipo_pago() { return this.garantiaForm.get('tipo_pago'); }
  get secuencial() { return this.garantiaForm.get('secuencial'); }
  get fecha_registro() { return this.garantiaForm.get('fecha_registro'); }
  get fechaReg() { return this.garantiaForm.get('fechaReg'); }
  get fechaAct() { return this.garantiaForm.get('fechaAct'); }
  get usuario() { return this.garantiaForm.get('usuario'); }

}

interface GData {
  cod_bl: string;
  nave: string;
  contenedores: string;
  tipo_contenedor: string;
  cod_container: string;
  consignatario: string;
  banco: string;
  cuenta: string;
  valor: number;
  cheque: string;
  tipo_pago: string;
}
