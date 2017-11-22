import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import {MatIconModule,MatIconRegistry} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';


// import { LoginComponent } from './login/login.module'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})


export class AppComponent implements OnInit{
  // login: LoginComponent;

  modes = [{link: "/user", text:"User Profile", icon:"account_box"}, 
    {link: "/lifts", text:"Lifts", icon:"timeline"}];

  constructor(public dialog: MatDialog, matIconRegistry: MatIconRegistry){
    this.openDialog();
  }

  openDialog(): void {
    let dialogRef = this.dialog.open(LoginDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  ngOnInit() {
  }

}