import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { CierreService } from '../services/cierreservice.service';

@Component({
    selector: 'fetchcierrei',
    templateUrl: './fetchcierrei.component.html'
})

export class FetchCierreIComponent {
    public cieiList: CierreIData[];

    constructor(public http: Http, private _router: Router, private _cierreService: CierreService) {
        this.getCierreI();
    }

    getCierreI() {
        this._cierreService.getCierreI().subscribe(
            data => this.cieiList = data
        )
    }
}

interface CierreIData {
    total_ingresos: number;
    fecha_registro: string;
    tipo_pago: string;
}
