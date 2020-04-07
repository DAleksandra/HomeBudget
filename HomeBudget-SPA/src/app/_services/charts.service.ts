import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';
import { Bar } from '../_models/bar';
import { Pie } from '../_models/pie';

@Injectable({
  providedIn: 'root'
})
export class ChartsService {
  baseUrl = 'http://localhost:5000/api/';

constructor(private http: HttpClient, private authService: AuthService) { }

getBars(userId: number, dateStart: string, dateEnd: string)
{
  let params = new HttpParams();
  params = params.append('DateStart', dateStart);
  params = params.append('DateEnd', dateEnd);

  return this.http.get<Bar>(this.baseUrl + userId + '/charts/bar', { observe: 'response', params}).pipe(
    map(response => {
      return response.body;
    })
  );
}

getPies(userId: number, dateStart: string, dateEnd: string)
{
  let params = new HttpParams();
  params = params.append('DateStart', dateStart);
  params = params.append('DateEnd', dateEnd);

  return this.http.get<Pie>(this.baseUrl + userId + '/charts/pie', { observe: 'response', params}).pipe(
    map(response => {
      return response.body;
    })
  );
}

}
