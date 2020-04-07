import { Component, OnInit } from '@angular/core';
import { IncomesService } from '../_services/incomes.service';
import { OutgoingsService } from '../_services/outgoings.service';
import { AlertifyService } from '../_services/alertify.service';
import { RecurringIncome } from '../_models/recurring-income';
import { RecurringOutgoing } from '../_models/recurring-outgoing';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  recurringIncomes: RecurringIncome[];
  recurringOutgoings: RecurringOutgoing[];

  constructor(private incomeService: IncomesService, private outgoingService: OutgoingsService,
              private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {

    this.loadIncome();

    this.loadOutgoing();
  }

  loadIncome() {
    this.incomeService.getRecurringIncomes()
    .subscribe((x: RecurringIncome[]) => {
      this.recurringIncomes = x;
  }, error => {
    console.log(error);
  });
}

loadOutgoing() {
    this.outgoingService.getRecurringOutgoings()
    .subscribe((x: RecurringOutgoing[]) => {
      this.recurringOutgoings = x;
    }, error => {
      console.log(error);
    });
  }

openRecurringOutgoing(id: number) {
  this.router.navigate(['profile/edit/editoutgoing'], { queryParams: { number: id }});
}

openRecurringIncome(id: number) {
  this.router.navigate(['profile/edit/editincome'], { queryParams: { number: id }});
}

}
