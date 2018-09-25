import { Component, Renderer2 } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent {

  user = { username: null, password: null, recordarme: false };
  mensaje: string;

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
    if (this.user.username != null && this.user.password != null) {
      this._loginService.userAuthentication(this.user.username, this.user.password).subscribe(
        data => {
          if (data == undefined || data == null) {
            this.mensaje = "No se ha podido comprobrar su credencial de acceso";
          } else {
            localStorage.setItem('usuario', JSON.stringify(data));
            this._router.navigate(['/home']);
            this.mensaje = "";
          }
        }
      );
    }
  }

}

