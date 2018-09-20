import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class ClienteBlService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getClientesBl() {
        return this._http.get(this.myAppUrl + 'api/ClienteBl/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }   

    getClienteBlById(id: number) {
        return this._http.get(this.myAppUrl + "api/ClienteBl/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    saveClienteBl(clientebl) {
        return this._http.post(this.myAppUrl + 'api/ClienteBl/Create', clientebl)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    updateClienteBl(clientebl) {
        return this._http.put(this.myAppUrl + 'api/ClienteBl/Edit', clientebl)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    deleteClienteBl(id) {
        return this._http.delete(this.myAppUrl + "api/ClienteBl/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}