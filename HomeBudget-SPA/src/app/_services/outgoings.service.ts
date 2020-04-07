import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Outgoing } from '../_models/outgoing';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';
import { RecurringOutgoing } from '../_models/recurring-outgoing';

@Injectable({
  providedIn: 'root'
})
export class OutgoingsService {
  baseUrl = 'http://localhost:5000/api/';

constructor(private http: HttpClient, private authService: AuthService) { }

getOutgoing(userId: number, id: number): Observable<Outgoing>
{
  return this.http.get<Outgoing>(this.baseUrl + this.authService.decodedToken.nameid + '/outgoings/' + id);
}

getOutgoings(userId: number, dateStart: string, dateEnd: string)
{
  let params = new HttpParams();
  params = params.append('DateStart', dateStart);
  params = params.append('DateEnd', dateEnd);

  return this.http.get<Outgoing[]>(this.baseUrl + userId + '/outgoings', { observe: 'response', params}).pipe(
    map(response => {
      return response.body;
    })
  );
}

updateOutgoing(userId: number, id: number, outgoing: Outgoing)
{
  console.log(this.baseUrl + this.authService.decodedToken.nameid + '/outgoings/' + id, outgoing);
  return this.http.put(this.baseUrl + this.authService.decodedToken.nameid + '/outgoings/' + id, outgoing);
}


addOutgoing(userId: number, outgoing: Outgoing)
{
  console.log(outgoing);
  return this.http.post(this.baseUrl + this.authService.decodedToken.nameid + '/outgoings', outgoing);
}

deleteOutgoing(userId: number, id: number) {
  return this.http.delete(this.baseUrl + this.authService.decodedToken.nameid + '/outgoings/' + id);
}



getRecurringOutgoing(id: number): Observable<RecurringOutgoing>
{
  return this.http.get<RecurringOutgoing>(this.baseUrl + this.authService.decodedToken.nameid + '/recurringoutgoings/' + id);
}

getRecurringOutgoings()
{
  return this.http.get<RecurringOutgoing[]>(this.baseUrl + this.authService.decodedToken.nameid + '/recurringoutgoings');
}

updateRecurringOutgoing(id: number, outgoing: RecurringOutgoing)
{
  console.log(this.baseUrl + this.authService.decodedToken.nameid + '/recurringoutgoings/' + id, outgoing);
  return this.http.put(this.baseUrl + this.authService.decodedToken.nameid + '/recurringoutgoings/' + id, outgoing);
}


addRecurringOutgoing(outgoing: RecurringOutgoing)
{
  console.log(outgoing);
  return this.http.post(this.baseUrl + this.authService.decodedToken.nameid + '/recurringoutgoings', outgoing);
}

deleteRecurringOutgoing(id: number) {
  return this.http.delete(this.baseUrl + this.authService.decodedToken.nameid + '/recurringoutgoings/' + id);
}

}
