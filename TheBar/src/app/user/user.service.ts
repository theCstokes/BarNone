import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './user'
import { Observable } from 'rxjs/Observable';
import { LoginService } from './../login/login.service';



@Injectable()
export class UserService {

  constructor(private loginService: LoginService, private http: HttpClient) {
    
   }

  getCurrentUser() : Observable<User[]> {
    let options = {
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.loginService.access_token)
    };

    console.log(options)

    return this.http.get<User[]>('/api/v1/User', options);
  }

}
