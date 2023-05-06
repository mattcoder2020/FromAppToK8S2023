import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryComponent } from '../inventory.component/inventory.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes =
  [
    { path: "", component: InventoryComponent }
 
  ]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [InventoryComponent]
})
export class InventorymoduleModule { }
