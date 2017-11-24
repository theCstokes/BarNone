import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { RouterModule, Routes } from '@angular/router';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([{path: 'user',  component: UserComponent }])
  ],
  declarations: [
    UserComponent
  ],
  exports:[
    UserComponent
  ]
})
export class UserModule { }
