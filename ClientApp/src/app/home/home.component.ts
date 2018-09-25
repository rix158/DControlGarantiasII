import { Component, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  name = 'Administrador';
  fecha = new Date();
  usuario;

  constructor(private router: Router, private loginService: LoginService, private _renderer: Renderer2) {
    this.changeClassHomePage();
  }

  Logout() {
    this.usuario = JSON.parse(localStorage.getItem('usuario'));
    //localStorage.removeItem('usuario');
    this.router.navigate(['/login']);
  }

  changeClassHomePage() {
    this._renderer.removeClass(document.body, 'simple-page');
    this._renderer.addClass(document.body, 'menubar-left');
    this._renderer.addClass(document.body, 'menubar-unfold');
    this._renderer.addClass(document.body, 'menubar-light');
    this._renderer.addClass(document.body, 'theme-primary');
    this._renderer.removeClass(document.getElementById('app-navbar'), 'delpac-hidden');
    this._renderer.removeClass(document.getElementById('menubar'), 'delpac-hidden');
  }


}
