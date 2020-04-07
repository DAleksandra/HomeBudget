import { Component, OnInit, Input, Output, EventEmitter, TemplateRef } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { BillsService } from 'src/app/_services/bills.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router, NavigationExtras } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-bill-card',
  templateUrl: './bill-card.component.html',
  styleUrls: ['./bill-card.component.css']
})
export class BillCardComponent implements OnInit {
  @Input() bill: Photo;
  @Output() billRemoved = new EventEmitter<number>();
  modalRef: BsModalRef;

  constructor(private billsService: BillsService, private alertify: AlertifyService, private router: Router,
              private modalService: BsModalService) { }

  ngOnInit() {
  }

  edit(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  remove() {
    this.billsService.deleteBill(this.bill.id).subscribe(x => {
      this.alertify.success('Entity successfully deleted!');
      this.router.navigate(['bills']);
    }, error => {
      this.alertify.error('Bill cannot be deleted!');
    });
    this.billRemoved.emit(this.bill.id);
  }

  update() {
    this.billsService.updateBill(this.bill).subscribe(x => {
      this.alertify.success('Bill edited sussessfully');
      this.modalRef.hide();
    }, error => {
      this.alertify.error('Bill cannot be edited');
    });
  }

  openCarousel(){
    this.billsService.billNow = this.bill.id;
    this.router.navigate(['bills/view']);
  }
  }
