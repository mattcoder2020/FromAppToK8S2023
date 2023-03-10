import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductlistComponent } from '../productlist.component/productlist.component';
import { ProductmodifyComponent } from '../productmodify.component/productmodify.component';
import { BasketComponent } from '../basket.component/basket.component';

const routes: Routes = 
  [
    { path: '', component: ProductlistComponent },
    { path: ':id', component: ProductmodifyComponent },
   
  ]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ProductRouteModule { }
