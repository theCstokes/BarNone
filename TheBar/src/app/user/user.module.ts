import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import {UserComponent} from './user.component';
import { RouterModule, Routes } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    MatCardModule, 
    MatTabsModule,
    RouterModule.forChild([{path: 'user',  component: UserComponent }])
  ],
  declarations: [


    UserComponent
  ]
})
export class UserModule { }
