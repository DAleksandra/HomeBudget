import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { IncomesService } from 'src/app/_services/incomes.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Income } from 'src/app/_models/income';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-income',
  templateUrl: './edit-income.component.html',
  styleUrls: ['./edit-income.component.css']
})
export class EditIncomeComponent implements OnInit {

  incomeId: number;
  income?: Income;
  categories =['Salary', 'Gifts', 'Others'];
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
    this.incomeService.getIncome(this.authService.decodedToken.nameid,
      +this.route.snapshot.queryParamMap.get('number')).subscribe(x => {
      this.income = x;
    }, error => {
      this.alertify.error(error);
    });
  }

  updateIncome() {
    this.incomeService.updateIncome(this.authService.decodedToken.nameid, this.income.id, this.income).subscribe(x => {
      this.alertify.success('Entity successfully updated!');
      this.router.navigate(['incomes']);
      this.editForm.reset(this.income);
    }, error => {
      this.alertify.error('Entity cannot be updated!');
    });
  }

  cancel() {
    this.router.navigate(['incomes']);
  }

  deleteIncome() {
    this.incomeService.deleteIncome(this.authService.decodedToken.nameid, this.income.id).subscribe(x => {
      this.alertify.success('Entity successfully deleted!');
      this.router.navigate(['incomes']);
      this.editForm.reset(this.income);
    }, error => {
      this.alertify.error('Entity cannot be deleted!');
    });
    this.router.navigate(['incomes']);
  }

}
