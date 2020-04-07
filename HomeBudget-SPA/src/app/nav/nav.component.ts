import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  welcomeUsername: string;

  constructor(public authService: AuthService, private router: Router, private alertify: AlertifyService) { 
    this.authService.welcomeUser.subscribe(value => {
      this.welcomeUsername = value;
    });
  }

  ngOnInit() {
    
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['']);
    this.alertify.message('logged out');
  }

}
