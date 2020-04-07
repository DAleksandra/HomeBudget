import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { OutgoingsComponent } from './outgoings/outgoings.component';
import { EditOutgoingComponent } from './outgoings/edit-outgoing/edit-outgoing.component';
import { NewOutgoingComponent } from './outgoings/new-outgoing/new-outgoing.component';
import { EditIncomeComponent } from './incomes/edit-income/edit-income.component';
import { NewIncomeComponent } from './incomes/new-income/new-income.component';
import { ChartsComponent } from './charts/charts.component';
import { IncomesComponent } from './incomes/incomes.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ProfileComponent } from './profile/profile.component';


import { AppRouting } from './app-routing';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule} from 'ngx-bootstrap';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AuthService } from './_services/auth.service';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ChartsModule } from 'ng2-charts';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BillsComponent } from './bills/bills.component';
import { BillCardComponent } from './bills/bill-card/bill-card.component';
import { NewBillComponent } from './bills/new-bill/new-bill.component';
import { FileUploadModule } from 'ng2-file-upload';
import { JwtModule } from '@auth0/angular-jwt';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BillPhotoPickerComponent } from './bills/bill-photo-picker/bill-photo-picker.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { EditRecurringIncomeComponent } from './edit-profile/edit-recurring-income/edit-recurring-income.component';
import { NewRecurringIncomeComponent } from './edit-profile/new-recurring-income/new-recurring-income.component';
import { NewRecurringOutgoingComponent } from './edit-profile/new-recurring-outgoing/new-recurring-outgoing.component';
import { EditRecurringOutgoingComponent } from './edit-profile/edit-recurring-outgoing/edit-recurring-outgoing.component';


export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      OutgoingsComponent,
      EditOutgoingComponent,
      NewOutgoingComponent,
      EditIncomeComponent,
      NewIncomeComponent,
      IncomesComponent,
      ChartsComponent,
      RegisterComponent,
      LoginComponent,
      EditProfileComponent,
      ProfileComponent,
      BillsComponent,
      BillCardComponent,
      NewBillComponent,
      BillPhotoPickerComponent,
      EditRecurringIncomeComponent,
      EditRecurringOutgoingComponent,
      NewRecurringIncomeComponent,
      NewRecurringOutgoingComponent
   ],
   imports: [
      BrowserModule,
      RouterModule.forRoot(AppRouting),
      FormsModule,
      ReactiveFormsModule,
      HttpClientModule,
      BrowserAnimationsModule,
      TabsModule.forRoot(),
      BsDatepickerModule.forRoot(),
      BsDropdownModule.forRoot(),
      ModalModule.forRoot(),
      ChartsModule,
      CarouselModule.forRoot(),
      FileUploadModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }),
   ],
   providers: [
      AuthService,
      PreventUnsavedChanges
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
