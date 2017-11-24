import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { LoginService } from './login.service'
import { FlexLayoutModule } from '@angular/flex-layout';

import { MatInputModule} from '@angular/material/input';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatButtonModule} from '@angular/material/button';
import { FormsModule } from '@angular/forms'; 

@NgModule({
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    FlexLayoutModule,
    MatButtonModule,
    FormsModule
  ],
  declarations: [LoginComponent],
  providers: [LoginService],
  exports: [LoginComponent]
})

export class LoginModule { 
  
}
