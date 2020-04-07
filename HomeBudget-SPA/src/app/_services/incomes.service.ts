import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Income } from '../_models/income';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { RecurringIncome } from '../_models/recurring-income';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {
baseUrl = 'http://localhost:5000/api/';

constructor(private http: HttpClient, private authService: AuthService) { }

getIncome(userId: number, id: number): Observable<Income>
{
  return this.http.get<Income>(this.baseUrl + this.authService.decodedToken.nameid + '/incomes/' + id);
}

getIncomes(userId: number, dateStart: string, dateEnd: string)
{
  let params = new HttpParams();
  params = params.append('DateStart', dateStart);
  params = params.append('DateEnd', dateEnd);

  return this.http.get<Income[]>(this.baseUrl + userId + '/incomes', { observe: 'response', params}).pipe(
    map(response => {
      return response.body;
    })
  );
}

updateIncome(userId: number, id: number, income: Income)
{
  console.log(this.baseUrl + this.authService.decodedToken.nameid + '/incomes/' + id, income);
  return this.http.put(this.baseUrl + this.authService.decodedToken.nameid + '/incomes/' + id, income);
}


addIncome(userId: number, income: Income)
{
  console.log(income);
  return this.http.post(this.baseUrl + this.authService.decodedToken.nameid + '/incomes', income);
}

deleteIncome(userId: number, id: number) {
  return this.http.delete(this.baseUrl + this.authService.decodedToken.nameid + '/incomes/' + id);
}

getRecurringIncome(id: number): Observable<RecurringIncome>
{
  return this.http.get<RecurringIncome>(this.baseUrl + this.authService.decodedToken.nameid + '/recurringincomes/' + id);
}

getRecurringIncomes()
{
  return this.http.get<RecurringIncome[]>(this.baseUrl + this.authService.decodedToken.nameid + '/recurringincomes');
}

updateRecurringIncome(id: number, recurringIncome: RecurringIncome)
{
  return this.http.put(this.baseUrl + this.authService.decodedToken.nameid + '/recurringincomes/' + id, recurringIncome);
}


addRecurringIncome(recurringIncome: RecurringIncome)
{
  return this.http.post(this.baseUrl + this.authService.decodedToken.nameid + '/recurringincomes', recurringIncome);
}

deleteRecurringIncome(id: number) {
  return this.http.delete(this.baseUrl + this.authService.decodedToken.nameid + '/recurringincomes/' + id);
}

}
