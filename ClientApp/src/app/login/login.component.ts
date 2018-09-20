import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent{

  //usuario: string = "";
  //password: string = "";
  //user: any = null;

  constructor(private _loginService: LoginService, private _router: Router) {

  }
    remove() {
      var elem = document.getElementById('backed');
      elem.parentNode.removeChild(elem);
      return false;
    }

  login() {
    this._loginService.login(this.usuario, this.password).subscribe(
      data => {
        this.user = data;
        if (this.user == undefined || this.user == null) {
          alert('NO SE HA PODIDO COMPROBAR SU CREDENCIAL DE ACCESO ');
        } else {
          this._router.navigate(['/home']);
        }
      }
    )
  }

}

