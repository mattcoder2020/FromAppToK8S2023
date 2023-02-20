import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductlistComponent } from '../productlist.component/productlist.component';
import { ProductmodifyComponent } from '../productmodify.component/productmodify.component';
const routes: Routes = 
  [
    { path: 'product', component: ProductlistComponent },
    { path: 'product/:id', component: ProductmodifyComponent, pathMatch: 'full'}
  ]

@NgModule({
  imports: [
    CommonModule, RouterModule.forRoot(routes)
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ProductRouteModule { }
