import { Injectable, } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from './../login/login.service';
import { Observable } from 'rxjs/Observable';
import { Lift } from './lifts'

export interface Joint {
  jointTypeID: number,
  x: number,
  y: number,
  z: number
}

@Injectable()
export class LiftsService {


  constructor(private http: HttpClient, private loginService: LoginService) {

  }

  getLifts() : Observable<object>{
    let options = {
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.loginService.access_token)
    };

    return this.http.get<object>('/api/v1/Lift', options)
  }

  getLiftDetails(id: number): Observable<object> {
    let options = {
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.loginService.access_token)
    };

    let url: string = '/api/v1/Lift/' + String(id) + '/Detail';
    console.log(url);

    return this.http.get(url, options);
  }

}
