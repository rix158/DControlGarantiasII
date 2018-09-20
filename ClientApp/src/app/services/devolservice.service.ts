import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class DevolucionService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getDevoluciones() {
        return this._http.get(this.myAppUrl + 'api/Devolucion/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getDevolucionesAp() {
        return this._http.get(this.myAppUrl + 'api/DevolucionAp/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getDevolucionById(id: number) {
        return this._http.get(this.myAppUrl + "api/Devolucion/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    getDevolucionApById(id: number) {
        return this._http.get(this.myAppUrl + "api/DevolucionAp/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }
    
    saveDevolucion(devolucion) {
        return this._http.post(this.myAppUrl + 'api/Devolucion/Create', devolucion)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
  }

  //REVISAR PROCESO DE EDICION - UPDATE DE CHEQUE POR REGISTRO
    updateDevolucion(id: number) {
        return this._http.get(this.myAppUrl + 'api/Devolucion/Edit/' + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    updateDevolucionAp(devolucionap) {
        return this._http.put(this.myAppUrl + 'api/DevolucionAp/Edit/', devolucionap)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}
