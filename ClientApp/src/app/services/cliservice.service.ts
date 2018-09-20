import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class ClienteService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getClientes() {
        return this._http.get(this.myAppUrl + 'api/Cliente/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getClienteById(id: number) {
        return this._http.get(this.myAppUrl + "api/Cliente/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    saveCliente(cliente) {
        return this._http.post(this.myAppUrl + 'api/Cliente/Create', cliente)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    } 

    updateCliente(cliente) {
        return this._http.put(this.myAppUrl + 'api/Cliente/Edit', cliente)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }
    
    deleteCliente(id) {
        return this._http.delete(this.myAppUrl + "api/Cliente/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }  

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}