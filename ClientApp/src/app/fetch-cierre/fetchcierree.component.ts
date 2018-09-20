import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CierreService } from '../services/cierreservice.service';

@Component({
    selector: 'fetchcierree',
    templateUrl: './fetchcierree.component.html'
})

export class FetchCierreEComponent {
    public cieeList: CierreEData[];

    constructor(public http: Http, private _router: Router, private _cierreService: CierreService) {
        this.getCierreE();
    }

    getCierreE() {
        this._cierreService.getCierreE().subscribe(
            data => this.cieeList = data
        )
    }
}

interface CierreEData {
    total_ingresos: number;
    fecha_registro: string;
    tipo_pago: string;
}
