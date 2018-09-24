import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class LoginService {
  myAppUrl: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  userAuthentication(userName, password) {
    var data = "username=" + userName + "&password=" + password + "&grant_type=password";
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True' });
    return this.http.post(this.myAppUrl + '/token', data, { headers: reqHeader });
    /*return this._http.get(this.myAppUrl + 'api/Login/Index?usuario=' + user + '&password=' + password)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
            */
  }

  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }

}
