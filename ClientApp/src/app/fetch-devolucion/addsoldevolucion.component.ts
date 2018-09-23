import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchDevolucionComponent } from '../fetch-devolucion/fetchdevolucion.component';
import { DevolucionService } from '../services/devolservice.service';
import { GarantiaService } from '../services/garanservice.service';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'createdevolucion',
  templateUrl: './addsoldevolucion.component.html'
})

export class createDevolucion implements OnInit {

  selected: string;
  listabl: string[] = [];
  selectedValue: string;
  selectedOption: any;

  public dList: DData[];
  devolucionForm: FormGroup;
  title: string = "Crear";
  id: number;
  errorMessage: any;
  fecha = new Date().toJSON().slice(0, 10);

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _devolucionService: DevolucionService,
    private _garantiaService: GarantiaService, private _router: Router) {
    this.getBlGarantias();

    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }

    this.devolucionForm = this._fb.group({
      id_devolucin: 0,
      cod_bl: ['', [Validators.required]],
      fecha_dev: ['', [Validators.required]],
      consignatario: ['', [Validators.required]],
      cliente: ['', [Validators.required]],
      email: ['', [Validators.required]],
      doc_recibo_cheque: [''],
      doc_EIR: [''],
      tipo_cliente: ['', [Validators.required]],
      motivo_multa: ['', [Validators.required]]
      
    })
  }

  /*Consulta los bls para el metodo de autocompletado*/
  getBlGarantias() {
    this._garantiaService.getBlGarantias().subscribe(
      data => {
        this.dList = data;
        this.listabl.splice(0, this.listabl.length);
        for (let i = 0; i < this.dList.length; i++) {
          this.listabl.push(this.dList[i].cod_bl);
        }
      }
    )
  }

  ngOnInit() {
    if (this.id > 0) {
      this.title = "Editar";
      this._devolucionService.getDevolucionById(this.id)
        .subscribe(resp => this.devolucionForm.setValue(resp)
          , error => this.errorMessage = error);
    }
  }

  save() {
    if (!this.devolucionForm.valid) {
      return;
    }

    if (this.title == "Crear") {
      this._devolucionService.saveDevolucion(this.devolucionForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-devolucion']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Editar") {
      this._devolucionService.updateDevolucion(this.devolucionForm.value)
        .subscribe((data) => {
          this._router.navigate(['/fetch-devolucion']);
        }, error => this.errorMessage = error)
    }
  }

  /*Metodo que carga los datos al digitar/setear el numero de bl*/
  onSelect(event: TypeaheadMatch): void {
    this.selectedOption = event.item;
    this._garantiaService.getPrevGarantiaById(this.selectedOption)
      .subscribe(resp => {
        let bl = {
          cod_bl: resp[0]['cod_bl'],
          consignatario: resp[0]['consignatario'],
          fecha_dev: this.fecha
        };
        this.devolucionForm.patchValue(bl); 
      }
        , error => this.errorMessage = error)
  }

  cancel() {
    this._router.navigate(['/fetch-devolucion']);
  }

  get id_devolucion() { return this.devolucionForm.get('id_devolucion'); }
  get cod_bl() { return this.devolucionForm.get('cod_bl'); }
  get fecha_dev() { return this.devolucionForm.get('fecha_dev'); }
  get consignatario() { return this.devolucionForm.get('consignatario'); }
  get cliente() { return this.devolucionForm.get('cliente'); }
  get email() { return this.devolucionForm.get('email'); }
  get doc_recibo_cheque() { return this.devolucionForm.get('doc_recibo_cheque'); }
  get doc_EIR() { return this.devolucionForm.get('doc_EIR'); }
  get tipo_cliente() { return this.devolucionForm.get('tipo_cliente'); }
  get motivo_multa() { return this.devolucionForm.get('motivo_multa'); }
  
}

interface DData {
  id_devolucion: number;
  cod_bl: string;  
  fecha_dev: string;
  consignatario: string;
  cliente: string;
  email: string;
  doc_recibo_cheque: string;
  doc_EIR: string;
  tipo_cliente: string;
  motivo_multa: string;
}
