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
    // var data = "username=" + userName + "&password=" + password;
    // var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded','No-Auth':'True' });
    return this.http.get(this.myAppUrl + 'api/Login/Index?usuario=' + userName + '&password=' + password);

  }

  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }

}
