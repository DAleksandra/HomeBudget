import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IncomesService } from 'src/app/_services/incomes.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-new-recurring-income',
  templateUrl: './new-recurring-income.component.html',
  styleUrls: ['./new-recurring-income.component.css']
})
export class NewRecurringIncomeComponent implements OnInit {
  incomeId: number;
  income?: any;
  categories = ['Salary', 'Gifts', 'Others'];
  intervals = ['Daily', 'Weekly', 'Monthly', 'Yearly'];
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if(this.editForm.dirty) {
      $event.returnValue = true;
    }
  } 
  @ViewChild('editForm, {static: true') editForm: NgForm;
  
  constructor(private route: ActivatedRoute, private incomeService: IncomesService,
    private authService: AuthService, private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
    this.income = {
      category: "Recurring",
      interval: "",
      description: "",
      source: "",
      amount: 0
    };
  }

  newIncome() {
    this.incomeService.addRecurringIncome(this.income).subscribe(x => {
      this.alertify.success('Entity successfully added!');
      this.router.navigate(['profile/edit']);
      this.editForm.reset(this.income);
    }, error => {
      this.alertify.error('Entity cannot be added!');
    });
  }

  cancel() {
    this.router.navigate(['profile/edit']);
  }

}
