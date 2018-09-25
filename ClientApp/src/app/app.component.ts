import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'  
})
export class AppComponent {
      
  constructor(private _router: Router){

  }

  logout(){
    localStorage.removeItem('usuario');
    this._router.navigate(['/login']);
  }
}

