import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchDevolucionComponent } from '../fetch-devolucion/fetchdevolucion.component';
import { GarantiaService } from '../services/garanservice.service';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';


@Component({
  selector: 'creategarantia',
  templateUrl: './addgarantia.component.html'
})

export class createGarantia implements OnInit {

  selected: string;
  listabl: string[] = [];
  selectedValue: string;
  selectedOption: any;
  cliente: string;
  cod_bl: string ="";

  private fieldArray: Array<any> = [];
  private newAttribute: any = {};
  public gList: GData[];
  public gListed: any[] = [];
  codbl: string;
  //garantiaForm: FormGroup;
  title: string = "Crear";
  id: number;
  errorMessage: any;

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _garantiaService: GarantiaService, private _router: Router) {
    this.getBlGarantias();
    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }

    //this.garantiaForm = this._fb.group({
    //  id_garantia: 0,
    //  cod_bl: ['', [Validators.required]],
    //  nave: ['', [Validators.required]],
    //  cliente: ['', [Validators.required]],
    //  banco: ['', [Validators.required]],
    //  numero_cuenta: ['', [Validators.required]],
    //  consignatario: ['', [Validators.required]],
    //  contenedores: ['', [Validators.required]],
    //  cod_container: ['', [Validators.required]],
    //  tipo_contenedor: ['', [Validators.required]],
    //  valor: ['', [Validators.required]],
    //  cheque: ['', [Validators.required]],
    //  tipo_pago: ['', [Validators.required]],
    //  fecha_registro: [''],
    //  usuario: [''],
    //  fechaReg: [''],
    //  fechaAct: ['']
    //})
  }

  /*Consulta los bls para el metodo de autocompletado*/
  getBlGarantias() {
    this._garantiaService.getBlGarantias().subscribe(
      data => {
        this.gList = data;
        this.listabl.splice(0, this.listabl.length);
        for (let i = 0; i < this.gList.length; i++) {
          this.listabl.push(this.gList[i].cod_bl);
        }
      }
    )
  }
  
  ngOnInit() {
    this.addFieldValue();
    if (this.id > 0) {
      this.title = "Editar";
      this._garantiaService.getGarantiaById(this.id)
        .subscribe(resp => {
          //this.garantiaForm.setValue(resp);
          //this.fieldArray[0] = {};
        }, error => this.errorMessage = error)
    }
  }

  guardar() { 
    let detalle: string = "";
    for (let i = 0; i < this.fieldArray.length; i++) {
      if (this.fieldArray[i].banco != null) {
        detalle += (this.fieldArray[i].banco + "|" + this.fieldArray[i].cuenta + "|" + this.fieldArray[i].valor + "|" + this.fieldArray[i].cheque + "|" +  this.fieldArray[i].tipo_pago +"|");
      }
      detalle += ";";
    }
    let itemGarantia = { bl: this.selectedOption, contenedores: this.gListed, cliente: this.cliente, detalle: detalle };
    if (this.title == "Crear") {
      this._garantiaService.saveGarantia(itemGarantia)
        .subscribe((data) => {
          this._router.navigate(['/fetch-garantia']);
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Editar") {
      this._garantiaService.updateGarantia(itemGarantia)
        .subscribe((data) => {
          this._router.navigate(['/fetch-garantia']);
        }, error => this.errorMessage = error)
    }
  }
  save() {
    //if (!this.garantiaForm.valid) {
    //  return;
    //}

    //if (this.title == "Crear") {
    //  this._garantiaService.saveGarantia(this.garantiaForm.value)
    //    .subscribe((data) => {
    //      this._router.navigate(['/fetch-garantia']);
    //    }, error => this.errorMessage = error)
    //}
    //else if (this.title == "Editar") {
    //  this._garantiaService.updateGarantia(this.garantiaForm.value)
    //    .subscribe((data) => {
    //      this._router.navigate(['/fetch-garantia']);
    //    }, error => this.errorMessage = error)
    //}
  }

  /*Metodo que carga los datos al digitar/setear el numero de bl*/
  onSelect(event: TypeaheadMatch): void {
    this.selectedOption = event.item;
    this._garantiaService.getPrevGarantiaById(this.selectedOption)
      .subscribe(resp => {
        this.gListed = resp;
      }
        , error => this.errorMessage = error)
  }


  addFieldValue() {
    this.newAttribute = {};
    this.fieldArray.push(this.newAttribute)
  }

  deleteFieldValue(index) {
    if (this.fieldArray.length > 1) {
      this.fieldArray.splice(index, 1);
    }
  }
  
  cancel() {
    this._router.navigate(['/fetch-garantia']);
  }

  //get cod_bl() { return this.garantiaForm.get('cod_bl'); }
  //get nave() { return this.garantiaForm.get('nave'); }
  //get cliente() { return this.garantiaForm.get('cliente'); }
  //get banco() { return this.garantiaForm.get('banco'); }
  //get numero_cuenta() { return this.garantiaForm.get('numero_cuenta'); }
  //get consignatario() { return this.garantiaForm.get('consignatario'); }
  //get contenedores() { return this.garantiaForm.get('contenedores'); }
  //get cod_container() { return this.garantiaForm.get('cod_container'); }
  //get tipo_contenedor() { return this.garantiaForm.get('tipo_contenedor'); }
  //get valor() { return this.garantiaForm.get('valor'); }
  //get cheque() { return this.garantiaForm.get('cheque'); }
  //get tipo_pago() { return this.garantiaForm.get('tipo_pago'); }
  //get fecha_registro() { return this.garantiaForm.get('fecha_registro'); }
  //get fechaReg() { return this.garantiaForm.get('fechaReg'); }
  //get fechaAct() { return this.garantiaForm.get('fechaAct'); }
  //get usuario() { return this.garantiaForm.get('usuario'); }

}

interface GData {
  cod_bl: string;
  cliente: string;
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
