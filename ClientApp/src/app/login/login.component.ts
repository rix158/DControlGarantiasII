import { Component, Renderer2 } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent {

  user={username:"",password:"",recordarme:''};

  constructor(private _loginService: LoginService, private _router: Router,private _renderer: Renderer2) {
    this._renderer.addClass(document.body, 'simple-page');
    this._renderer.removeClass(document.body, 'menubar-left');
    this._renderer.removeClass(document.body, 'menubar-unfold');
    this._renderer.removeClass(document.body, 'menubar-light');
    this._renderer.removeClass(document.body, 'theme-primary');
    this._renderer.addClass(document.getElementById('app-navbar'), 'delpac-hidden');
    this._renderer.addClass(document.getElementById('menubar'), 'delpac-hidden');
  }
  
  remove() {
    var elem = document.getElementById('backed');
    elem.parentNode.removeChild(elem);
    return false;
  }

  login() {
    /*this._loginService.userAuthentication(this.usuario.name, this.usuario.password).subscribe(
      data => {
        this.user = data;
        if (this.user == undefined || this.user == null) {
          this.mensaje = 'NO SE HA PODIDO COMPROBAR SU CREDENCIAL DE ACCESO ';
        } else {
          this._router.navigate(['/home']);
          this.mensaje = "";
        }
      }
    )*/
    this._router.navigate(['/home']);
  }

}

