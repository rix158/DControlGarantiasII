import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchClienteComponent } from './fetchcliente.component';
import { ClienteService } from '../services/cliservice.service';

@Component({
    selector: 'createcliente',
    templateUrl: './addcliente.component.html'
})

export class createcliente implements OnInit {

    clienteForm: FormGroup;
    title: string = "Crear";
    id: number;
    errorMessage: any;

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _clienteService: ClienteService, private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {
            this.id = this._avRoute.snapshot.params["id"];
        }

        this.clienteForm = this._fb.group({
            id: 0,
            ruc: ['', [Validators.required]],
            cliente: ['', [Validators.required]],
            exoneracion: ['', [Validators.required]],
            observacion: ['', [Validators.required]],
            cod_bl: [''],
            usuario: [''],
            fechaReg: [''],
            fechaAct: [''],
        })
    }

    ngOnInit() {
        if (this.id > 0) {
            this.title = "Editar";
            this._clienteService.getClienteById(this.id)
                .subscribe(resp => this.clienteForm.setValue(resp)
                    , error => this.errorMessage = error);
        }
    }

    save() {
        if (!this.clienteForm.valid) {
            return;
        }

        if (this.title == "Crear") {
            this._clienteService.saveCliente(this.clienteForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-cliente']);
                }, error => this.errorMessage = error)
        }
        else if (this.title == "Editar") {
            this._clienteService.updateCliente(this.clienteForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-cliente']);
                }, error => this.errorMessage = error)
        }
    }

    cancel() {
        this._router.navigate(['/fetch-cliente']);
    }

    get ruc() { return this.clienteForm.get('ruc'); }
    get cliente() { return this.clienteForm.get('cliente'); }
    get exoneracion() { return this.clienteForm.get('exoneracion'); }
    get observacion() { return this.clienteForm.get('observacion'); }
}
