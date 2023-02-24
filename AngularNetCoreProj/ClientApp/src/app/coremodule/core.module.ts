import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
import { ErrorComponent } from './error/error.component';

@NgModule({
  declarations: [
    NavMenuComponent,
    ErrorComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot([{ path: "error", component: ErrorComponent }])
  ],
  exports: [NavMenuComponent, RouterModule]
})
export class CoreModule { }
