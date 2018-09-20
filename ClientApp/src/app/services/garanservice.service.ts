import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class GarantiaService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getGarantias() {
        return this._http.get(this.myAppUrl + 'api/Garantia/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getGarantiaById(id: number) {
        return this._http.get(this.myAppUrl + "api/Garantia/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    saveGarantia(garantia) {
        return this._http.post(this.myAppUrl + 'api/Garantia/Create', garantia)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    updateGarantia(garantia) {
        return this._http.put(this.myAppUrl + 'api/Garantia/Edit', garantia)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    deleteGarantia(id) {
        return this._http.delete(this.myAppUrl + "api/Garantia/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
  }

  getBlGarantias() {
    return this._http.get(this.myAppUrl + 'api/GarantiaBl/Index')
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  getPrevGarantiaById(cod_bl: string) {
    return this._http.get(this.myAppUrl + "api/Garantia/PrevDetails/" + cod_bl)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}
