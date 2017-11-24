import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import {LiftsComponent } from './lifts.component';

import {LiftsService} from './lifts.service';

@NgModule({
  imports: [
    CommonModule,
    MatCardModule, 
    MatGridListModule,
    MatButtonModule 
  ],
  providers: [LiftsService],
  declarations: [
  LiftsComponent
  ]
})
export class LiftsModule { }
