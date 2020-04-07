import { Component, OnInit, Input, ChangeDetectionStrategy, AfterViewChecked } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { BillsService } from 'src/app/_services/bills.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-bill-photo-picker',
  templateUrl: './bill-photo-picker.component.html',
  styleUrls: ['./bill-photo-picker.component.css']
})
export class BillPhotoPickerComponent implements OnInit {
  bills: Photo[];
  firstSlide: number;
  index: number;
  myInterval = 1000000;

  constructor(private billsService: BillsService) { 
    this.bills = this.billsService.bills;
    this.firstSlide = this.billsService.billNow;

  }

  ngOnInit() {
    
  }

}
