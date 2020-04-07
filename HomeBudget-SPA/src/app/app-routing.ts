import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { OutgoingsComponent } from './outgoings/outgoings.component';
import { NewOutgoingComponent } from './outgoings/new-outgoing/new-outgoing.component';
import { EditOutgoingComponent } from './outgoings/edit-outgoing/edit-outgoing.component';
import { IncomesComponent } from './incomes/incomes.component';
import { NewIncomeComponent } from './incomes/new-income/new-income.component';
import { EditIncomeComponent } from './incomes/edit-income/edit-income.component';
import { ChartsComponent } from './charts/charts.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { BillsComponent } from './bills/bills.component';
import { NewBillComponent } from './bills/new-bill/new-bill.component';
import { BillPhotoPickerComponent } from './bills/bill-photo-picker/bill-photo-picker.component';
import { EditRecurringIncomeComponent } from './edit-profile/edit-recurring-income/edit-recurring-income.component';
import { NewRecurringIncomeComponent } from './edit-profile/new-recurring-income/new-recurring-income.component';
import { EditRecurringOutgoingComponent } from './edit-profile/edit-recurring-outgoing/edit-recurring-outgoing.component';
import { NewRecurringOutgoingComponent } from './edit-profile/new-recurring-outgoing/new-recurring-outgoing.component';
import { Auth } from './_guards/auth.guard';

export const AppRouting: Routes = [
    {path: '', component: HomeComponent},
    {path: 'home', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [Auth],
        children: [
            {path: 'outgoings', component: OutgoingsComponent},
            {path: 'profile/edit', component: EditProfileComponent},
            {path: 'login', component: LoginComponent},
            {path: 'outgoings/new', component: NewOutgoingComponent},
            {path: 'outgoings/edit', component: EditOutgoingComponent, canDeactivate: [PreventUnsavedChanges]},
            {path: 'incomes', component: IncomesComponent},
            {path: 'incomes/new', component: NewIncomeComponent},
            {path: 'incomes/edit', component: EditIncomeComponent},
            {path: 'charts', component: ChartsComponent},
            {path: 'register', component: RegisterComponent},
            {path: 'bills', component: BillsComponent},
            {path: 'bills/new', component: NewBillComponent},
            {path: 'bills/view', component: BillPhotoPickerComponent},
            {path: 'profile/view', component: BillPhotoPickerComponent},
            {path: 'profile/edit/editincome', component: EditRecurringIncomeComponent},
            {path: 'profile/edit/newincome', component: NewRecurringIncomeComponent},
            {path: 'profile/edit/editoutgoing', component: EditRecurringOutgoingComponent},
            {path: 'profile/edit/newoutgoing', component: NewRecurringOutgoingComponent}
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
]
