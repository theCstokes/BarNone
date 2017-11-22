import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'bn-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;

  loginClicked(){
    if (this.username == "u" && this.password == "p"){
      this.username = "this is a name";
    }
  }

  constructor() { }

  ngOnInit() {
  }

}
