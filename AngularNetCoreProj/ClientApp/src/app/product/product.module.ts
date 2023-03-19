import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { ProductDisplayComponent } from './productdisplay.component/productdisplay.component';
import { ProductlistComponent } from './productlist.component/productlist.component';
import { ProductmodifyComponent } from './productmodify.component/productmodify.component';
import { ProductRouteModule } from './product-route/product-route.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BasketComponent } from './basket.component/basket.component';
import { CheckoutComponentComponent } from './checkout.component/checkout.component.component';

@NgModule({
  imports: [
    CommonModule, RouterModule, FormsModule, ProductRouteModule,
    NgxSpinnerModule.forRoot()

  ],
  declarations: [ProductDisplayComponent, ProductlistComponent, ProductmodifyComponent, BasketComponent, CheckoutComponentComponent],
  exports: [ProductDisplayComponent, ProductlistComponent]
})
export class Productmodule { }
