import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import { LiftsComponent } from './lifts.component';
import { LiftsService } from './lifts.service';
import { RouterModule, Routes } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';




@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    MatGridListModule,
    MatButtonModule,
    FlexLayoutModule,
    RouterModule.forChild([{ path: 'lifts', component: LiftsComponent }])
  ],
  providers: [LiftsService],
  declarations: [
    LiftsComponent
  ]
})
export class LiftsModule { }