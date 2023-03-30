import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductlistComponent } from '../productlist.component/productlist.component';
import { ProductmodifyComponent } from '../productmodify.component/productmodify.component';
import { BasketComponent } from '../basket.component/basket.component';
import { NewproductComponent } from '../newproduct.component/newproduct.component';

const routes: Routes = 
  [
    { path: '', component: ProductlistComponent },
    { path: ':id', component: ProductmodifyComponent },
    //add a element to the route that will route to newproduct component when the path is exactly "newproduct"


    { path: 'newproduct', component: NewproductComponent }
    //add a element to the route that will route to newproduct component when the path is "newproduct"


       
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
