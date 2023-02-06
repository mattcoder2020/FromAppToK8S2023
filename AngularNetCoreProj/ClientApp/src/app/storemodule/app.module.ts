import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { Store } from './store/store';
import { ProductList } from './productlist/productlist';
import { checkout } from './Checkout/Checkout';
import { cart } from './cart/cart';
import { dataservice } from './share/dataservice';
import { StarsComponent } from './productlist/stars/stars.component';

@NgModule({
  declarations: [
    Store,
    ProductList,
    checkout,
    cart,
    StarsComponent
  ],
  imports: [
    RouterModule.forRoot([
      { path: 'store', component: Store },
      { path: 'checkout', component: checkout },
     
    ])
  ],
  exports: [
    Store,
    ProductList,
    checkout,
    cart,
    StarsComponent
  ]
  
})
export class StoreModule { }
