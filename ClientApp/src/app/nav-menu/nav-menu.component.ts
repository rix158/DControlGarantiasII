import { Component } from '@angular/core';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html'
})
export class NavMenuComponent {
  usuario;

  constructor() {
    this.usuario = JSON.parse(localStorage.getItem('usuario'));
  }
}
