import { Component, OnInit } from '@angular/core';
import { LoginService } from './../login/login.service';
import { UserService } from './user.service'
import { User } from './user'



@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {


  user = {
    name: "Jonathan Brown",
    username: "admin"
  }

  users : User[]= null;  

  constructor(private loginService: LoginService, private userService: UserService) {
   
  }

  ngOnInit() {
    if (this.loginService.access_token != null) {
      this.userService.getCurrentUser().subscribe(value => this.users = value);
    }
    else {
      this.loginService.user_authenticated.subscribe(() => {
        this.userService.getCurrentUser().subscribe(value => {
          console.log(value); this.users = value;
        });
      })

    }
  }

}
