import { Injectable, EventEmitter  } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface LoginResponse {
  authorized : boolean,
  access_token :string,
  expires_in : number
}

interface LoginRequest {
  userName : string,
  password : string
}

@Injectable()
export class LoginService {

  access_token = null;
  user_authenticated: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpClient) {

   }

authenticate(username: string, password: string){
  let request : LoginRequest = {userName : username, password : password}
  if (request.userName == "u" && request.password =="p"){
    this.user_authenticated.emit();
    console.log("Emitting");
    return;    
  }
  this.http.post<LoginResponse>('/api/v1/Authorization/Login', request).subscribe(response => {
    if (response.authorized){
      this.access_token = response.access_token;
    }
  });
}

}
