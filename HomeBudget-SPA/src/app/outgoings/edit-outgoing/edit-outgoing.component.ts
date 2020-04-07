import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { OutgoingsService } from 'src/app/_services/outgoings.service';
import { Outgoing } from 'src/app/_models/outgoing';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-outgoing',
  templateUrl: './edit-outgoing.component.html',
  styleUrls: ['./edit-outgoing.component.css']
})
export class EditOutgoingComponent implements OnInit {
  outgoingId: number;
  outgoing?: Outgoing;
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
    this.outgoingService.getOutgoing(this.authService.decodedToken.nameid,
      +this.route.snapshot.queryParamMap.get('number')).subscribe(x => {
      this.outgoing = x;
    }, error => {
      this.alertify.error(error);
    });
  }

  updateOutgoing() {
    this.outgoingService.updateOutgoing(this.authService.decodedToken.nameid, this.outgoing.id, this.outgoing).subscribe(x => {
      this.alertify.success('Entity successfully updated!');
      this.router.navigate(['outgoings']);
      this.editForm.reset(this.outgoing);
    }, error => {
      this.alertify.error('Entity cannot be updated!');
    });
  }

  cancel() {
    this.router.navigate(['outgoings']);
  }

  deleteOutgoing() {
    this.outgoingService.deleteOutgoing(this.authService.decodedToken.nameid, this.outgoing.id).subscribe(x => {
      this.alertify.success('Entity successfully deleted!');
      this.router.navigate(['outgoings']);
      this.editForm.reset(this.outgoing);
    }, error => {
      this.alertify.error('Entity cannot be deleted!');
    })
    this.router.navigate(['outgoings']);
  }

}
