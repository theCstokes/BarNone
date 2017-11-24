import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService }  from './../login/login.service';

@Injectable()
export class LiftsService {

  constructor(private http : HttpClient, private loginService: LoginService) { 
  
  }

  getLifts(){
    let options = {
        headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.loginService.access_token)
    };

    console.log(options)
    
    this.http.get('/api/v1/Lifts', options).subscribe(response => {
    console.log(response);
    });
  }

}
