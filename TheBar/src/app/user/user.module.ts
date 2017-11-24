import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { UserComponent } from './user.component';
import { RouterModule, Routes } from '@angular/router';
import { UserService } from './user.service';
import { FlexLayoutModule } from '@angular/flex-layout';


@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    MatTabsModule,
    FlexLayoutModule,
    RouterModule.forChild([{ path: 'user', component: UserComponent }])
  ],
  providers: [UserService],
  declarations: [
    UserComponent
  ]
})
export class UserModule { }
