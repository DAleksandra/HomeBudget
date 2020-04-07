import { Component, OnInit } from '@angular/core';
import { OutgoingsService } from '../_services/outgoings.service';
import { Outgoing } from '../_models/outgoing';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-outgoings',
  templateUrl: './outgoings.component.html',
  styleUrls: ['./outgoings.component.css']
})
export class OutgoingsComponent implements OnInit {
  outgoingsArray: Outgoing[];
  currentDay = new Date();
  outgoingsSum: number = 0;
  firstDay: Date;
  lastDay: Date;
  showSpinner: boolean;

  constructor(private outgoings: OutgoingsService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.firstDay = new Date(this.currentDay.getFullYear(),this.currentDay.getMonth(), 1);
    this.lastDay = new Date(this.currentDay.getFullYear(), this.currentDay.getMonth() + 1, 0, 23, 59, 59);

    this.load();
  }

  loadEdit(id: number)
  {
    this.router.navigate(['outgoings/edit/'], { queryParams: { number: id }});
  }

  load() {
    this.showSpinner = true;
    this.outgoings.getOutgoings(this.authService.decodedToken.nameid, this.firstDay.toLocaleDateString(), this.lastDay.toLocaleDateString())
    .subscribe((x: Outgoing[]) => {
          this.outgoingsArray = x;
          this.showSpinner = false;
          this.outgoingsArray.forEach(entry => {
            this.outgoingsSum = this.outgoingsSum + entry.cost;
          });
      }, error => {
        console.log(error);
        this.showSpinner = false;
      });
  }

}
