import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-login-dialog',
  template: '<bn-login></bn-login>',
  // styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>) {
      console.log("Dialog created!");
     }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
