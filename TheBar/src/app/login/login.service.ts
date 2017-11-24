import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

interface LoginResponse {
  authorized: boolean,
  access_token: string,
  expires_in: number
}

interface LoginRequest {
  userName: string,
  password: string
}

@Injectable()
export class LoginService {

  access_token = null;
  user_authenticated: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpClient) {

  }

  authenticate(username: string, password: string) {
    let request: LoginRequest = { userName: username, password: password }
    let body = new URLSearchParams();
    body.set('userName', username);
    body.set('password', password);

    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };

    this.http.post<LoginResponse>('/api/v1/Authorization/Login', body.toString(), options).subscribe(response => {
      console.log(response);
      if (response.authorized) {
        this.access_token = response.access_token;
        this.user_authenticated.emit();
      }
    });
  }

}
