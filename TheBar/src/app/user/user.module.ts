import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import {UserComponent} from './user.component';
@NgModule({
  imports: [
    CommonModule,
    MatCardModule, 
    MatTabsModule
  ],
  declarations: [
    UserComponent
  ]
})
export class UserModule { }
