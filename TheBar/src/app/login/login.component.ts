import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service'

@Component({
  selector: 'bn-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;

  loginClicked() {
    this.loginService.authenticate(this.username, this.password);
  }

  constructor(private loginService: LoginService) { }

  ngOnInit() {
  }

}
