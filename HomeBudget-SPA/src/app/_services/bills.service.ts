import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Photo } from '../_models/photo';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  baseUrl = 'http://localhost:5000/api/users/';
  bills: Photo[];
  billNow: number;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getBills(dateStart: string, dateEnd: string) {

    let params = new HttpParams();
    params = params.append('DateStart', dateStart);
    params = params.append('DateEnd', dateEnd);

    return this.http.get<Photo[]>(this.baseUrl + this.authService.decodedToken.nameid + '/bills', { observe: 'response', params}).pipe(
      map(response => {
        return response.body;
      })
    );
  }

  deleteBill(id: number) {
    return this.http.delete(this.baseUrl + this.authService.decodedToken.nameid + '/bills/' + id);
  }

  updateBill(bill: Photo) {
    return this.http.put(this.baseUrl + this.authService.decodedToken.nameid + '/bills/' + bill.id, bill);
  }

}
