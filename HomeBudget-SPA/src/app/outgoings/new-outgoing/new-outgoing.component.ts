import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { OutgoingsService } from 'src/app/_services/outgoings.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Outgoing } from 'src/app/_models/outgoing';
import { AuthService } from 'src/app/_services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-new-outgoing',
  templateUrl: './new-outgoing.component.html',
  styleUrls: ['./new-outgoing.component.css']
})
export class NewOutgoingComponent implements OnInit {
  outgoingId: number;
  outgoing?: any;
  categories =['Food', 'Cosmetics', 'Clothes', 'Pharmacies', 'Entertainments', 'Books', 'Bills', 'Others'];
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if(this.editForm.dirty) {
      $event.returnValue = true;
    }
  } 
  @ViewChild('editForm, {static: true') editForm: NgForm;

  constructor(private route: ActivatedRoute, private outgoingService: OutgoingsService,
      private authService: AuthService, private alertify: AlertifyService,
      private router: Router) { }
     

  ngOnInit() {
    this.outgoing = {
      category: "",
      description: "",
      shop: "",
      cost: 0
    }
  }

  newOutgoing() {
    this.outgoingService.addOutgoing(this.authService.decodedToken.nameid, this.outgoing).subscribe(x => {
      this.alertify.success('Entity successfully added!');
      this.router.navigate(['outgoings']);
      this.editForm.reset(this.outgoing);
    }, error => {
      this.alertify.error('Entity cannot be added!');
    });
  }

  cancel() {
    this.router.navigate(['outgoings']);
  }

}
