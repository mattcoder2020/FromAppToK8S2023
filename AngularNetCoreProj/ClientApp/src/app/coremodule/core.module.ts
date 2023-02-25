import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './error/error.component';
import { ExceptionviewComponent } from './error/exceptionview/exceptionview.component';

const routes: Routes =
  [
    { path: "error", component: ErrorComponent },
    { path: "exceptionview", component: ExceptionviewComponent }
  ]

@NgModule({
  declarations: [
    NavMenuComponent,
    ErrorComponent,
    ExceptionviewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [NavMenuComponent, RouterModule]
})
export class CoreModule { }
