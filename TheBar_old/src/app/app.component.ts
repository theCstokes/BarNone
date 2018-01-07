import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { MatIconRegistry } from '@angular/material/icon';
import { LoginService } from './login/login.service'


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})


export class AppComponent implements OnInit {
  // login: LoginComponent;

  modes = [{ link: "/user", text: "User Profile", icon: "account_box" },
  { link: "/lifts", text: "Lifts", icon: "timeline" }];

  constructor(private loginService: LoginService, public dialog: MatDialog, matIconRegistry: MatIconRegistry) {
    this.openDialog();
    this.loginService.user_authenticated.subscribe(() => { console.log("received emit"); this.dialog.closeAll(); });
  }

  openDialog(): void {
    let dialogRef = this.dialog.open(LoginDialogComponent, {});
    dialogRef.disableClose = true;
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  ngOnInit() {
  }

}