import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LoginModule } from './login/login.module';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { LiftsComponent } from './lifts/lifts.component';
import { WelcomeComponent } from './welcome/welcome.component';
import {MatToolbarModule} from '@angular/material/toolbar';

const routes = [
  {path: 'user', component: UserComponent},
  {path: 'lifts', component: LiftsComponent},
  { path: '', component: WelcomeComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    LoginDialogComponent,
    UserComponent,
    LiftsComponent,
    WelcomeComponent
  ],
  imports: [
    BrowserModule,
    MatDialogModule,
    BrowserAnimationsModule,
    LoginModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatToolbarModule,
    RouterModule.forRoot(routes,{ enableTracing: true })
  ],
  providers: [MatIconRegistry],
  entryComponents: [LoginDialogComponent],
  bootstrap: [AppComponent]
})

export class AppModule { }
