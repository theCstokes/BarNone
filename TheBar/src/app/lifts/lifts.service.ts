import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from './../login/login.service';


export interface Joint{
  jointTypeID: number,
  x: number,
  y: number,
  z: number
}

@Injectable()
export class LiftsService {


  constructor(private http: HttpClient, private loginService: LoginService) {

  }

  getLifts() {
    let options = {
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.loginService.access_token)
    };

    console.log(options)

    this.http.get('/api/v1/Lift', options).subscribe(response => {
      console.log(response);
    });
  }

  getLiftDetails(id : number) : Joint[] {
    let options = {
      headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.loginService.access_token)
    };

    let url : string = '/api/v1/Lift/' + String(id) + '/Detail';
    console.log(url);
    

    this.http.get(url, options).subscribe(response => {
      console.log(response["entity"].details.bodyData.details.orderedFrames[0].details.joints);
      return response["entity"].details.bodyData.details.orderedFrames[0].details.joints
    });
    return null;
  }

}
