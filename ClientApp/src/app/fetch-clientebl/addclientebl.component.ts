import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchClienteBlComponent } from '../fetch-clientebl/fetchclientebl.component';
import { ClienteBlService } from '../services/cliblservice.service';

@Component({
    selector: 'createclientebl',
    templateUrl: './addclientebl.component.html'
})

export class createclientebl implements OnInit {

    clienteblForm: FormGroup;
    title: string = "Crear";
    id: number;
    errorMessage: any;

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _clienteblService: ClienteBlService, private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {
            this.id = this._avRoute.snapshot.params["id"];
        }

        this.clienteblForm = this._fb.group({
            id: 0,
            ruc: ['', [Validators.required]],
            cliente: ['', [Validators.required]],
            cod_bl: ['', [Validators.required]],
            exoneracion: ['', [Validators.required]],
            observacion: ['', [Validators.required]],
            usuario: [''],
            fechaReg: [''],
            fechaAct: ['']
        })
    }

    ngOnInit() {
        if (this.id > 0) {
            this.title = "Editar";
            this._clienteblService.getClienteBlById(this.id)
                .subscribe(resp => this.clienteblForm.setValue(resp)
                    , error => this.errorMessage = error);
        }
    }

    save() {
        if (!this.clienteblForm.valid) {
            return;
        }
            
        if (this.title == "Crear") {
            this._clienteblService.saveClienteBl(this.clienteblForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-clientebl']);
                }, error => this.errorMessage = error)
        }
        else if (this.title == "Editar") {
            this._clienteblService.updateClienteBl(this.clienteblForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-clientebl']);
                }, error => this.errorMessage = error)
        }
    }

    cancel() {
        this._router.navigate(['/fetch-clientebl']);
    }
    
    get ruc() { return this.clienteblForm.get('ruc'); }
    get cliente() { return this.clienteblForm.get('cliente'); }
    get cod_bl() { return this.clienteblForm.get('cod_bl'); }
    get exoneracion() { return this.clienteblForm.get('exoneracion'); }
    get observacion() { return this.clienteblForm.get('observacion'); }
    //get usuario() { return this.clienteblForm.get('usuario'); }
    //get fechaReg() { return this.clienteblForm.get('fechaReg'); }
    //get fechaAct() { return this.clienteblForm.get('fechaAct'); }
}
