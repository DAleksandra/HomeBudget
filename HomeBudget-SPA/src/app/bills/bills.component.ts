import { Component, OnInit } from '@angular/core';
import { Photo } from '../_models/photo';
import { User } from '../_models/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { BillsService } from '../_services/bills.service';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.css']
})
export class BillsComponent implements OnInit {
  bills: Photo[];
  currentDay = new Date();
  firstDay: Date;
  lastDay: Date;

  constructor(private authService: AuthService, private billsService: BillsService) {
   }

  ngOnInit() {
    this.firstDay = new Date(this.currentDay.getFullYear(),this.currentDay.getMonth(), 1);
    this.lastDay = new Date(this.currentDay.getFullYear(), this.currentDay.getMonth() + 1, 0, 23, 59, 59);
    
    this.load();
  }

  load() {
    this.billsService.getBills(this.firstDay.toLocaleDateString(), this.lastDay.toLocaleDateString())
    .subscribe((x: Photo[]) => {
      this.billsService.bills = x;
      this.bills = x;
  }, error => {
    console.log(error);
  });
  }

  billRemoved(id: number) {
    this.bills = this.bills.filter(item => item.id !== id);

  }

  carouselOpen(id: number) {
    this.bills = this.bills.filter(item => item.id !== id);
  }



}
