import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IncomesService } from 'src/app/_services/incomes.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { RecurringIncome } from 'src/app/_models/recurring-income';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-recurring-income',
  templateUrl: './edit-recurring-income.component.html',
  styleUrls: ['./edit-recurring-income.component.css']
})
export class EditRecurringIncomeComponent implements OnInit {
  incomeId: number;
  income?: RecurringIncome;
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
    this.incomeService.getRecurringIncome(+this.route.snapshot.queryParamMap.get('number')).subscribe(x => {
      this.income = x;
    }, error => {
      this.alertify.error(error);
    });
  }

  updateIncome() {
    this.incomeService.updateRecurringIncome(this.income.id, this.income).subscribe(x => {
      this.alertify.success('Entity successfully updated!');
      this.router.navigate(['profile/edit']);
      this.editForm.reset(this.income);
    }, error => {
      this.alertify.error('Entity cannot be updated!');
    });
  }

  cancel() {
    this.router.navigate(['profile/edit']);
  }

  deleteIncome() {
    this.incomeService.deleteRecurringIncome(this.income.id).subscribe(x => {
      this.alertify.success('Entity successfully deleted!');
      this.router.navigate(['profile/edit']);
      this.editForm.reset(this.income);
    }, error => {
      this.alertify.error('Entity cannot be deleted!');
    });
    this.router.navigate(['profile/edit']);
  }

}
