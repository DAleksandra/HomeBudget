import { Component, OnInit } from '@angular/core';
import { Income } from '../_models/income';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { Outgoing } from '../_models/outgoing';
import { IncomesService } from '../_services/incomes.service';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {
 incomesArray: Income[];
 currentDay = new Date();
 incomesSum: number = 0;
 firstDay: Date;
 lastDay: Date;
 showSpinner: boolean;

  constructor(private incomes: IncomesService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.firstDay = new Date(this.currentDay.getFullYear(),this.currentDay.getMonth(), 1);
    this.lastDay = new Date(this.currentDay.getFullYear(), this.currentDay.getMonth() + 1, 0, 23, 59, 59);

    this.load();
  }

  loadEdit(id: number)
  {
    this.router.navigate(['incomes/edit/'], { queryParams: { number: id }});
  }

  load() {
    this.showSpinner = true;
    this.incomes.getIncomes(this.authService.decodedToken.nameid, this.firstDay.toLocaleDateString(), this.lastDay.toLocaleDateString())
    .subscribe((x: Income[]) => {
      this.incomesArray = x;
      this.showSpinner = false;
      this.incomesArray.forEach(entry => {
        this.incomesSum = this.incomesSum + entry.amount;
      });
  }, error => {
    console.log(error);
    this.showSpinner = false;
  });
  }

}
