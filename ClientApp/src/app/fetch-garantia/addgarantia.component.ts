import { Component, OnInit, Renderer2 } from '@angular/core';
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
  cod_bl: string = "";
  label_titulo = "";

  private fieldArray: Array<any> = [];
  private newAttribute: any = {};
  public gList: GData[];
  public gListed: any[] = [];
  codbl: string;
  title: string = "Crear";
  id: number;
  errorMessage: any;

  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
    private _garantiaService: GarantiaService, private _router: Router, private _render: Renderer2) {
    this.getBlGarantias();
    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }
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
    this._render.removeClass(document.getElementById('searchcomp'), 'delpac-hidden');
    this.label_titulo = "Registro de Garantia";
    if (this.id > 0) {
      this.title = "Editar";
      this._garantiaService.getGarantiaById(this.id)
        .subscribe(resp => {
         
          this._render.addClass(document.getElementById('searchcomp'), 'delpac-hidden');
          this.label_titulo = "Edicion de Garantia del BL: " + resp['cod_bl'];
          this._garantiaService.getPrevGarantiaById(resp['cod_bl'])
            .subscribe(resp => {
              this.gListed = resp;
            }
              , error => this.errorMessage = error)
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
