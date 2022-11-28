import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { MyComponent } from './test-component/test.component';
import { Store } from './store/store';
import { ProductList } from './productlist/productlist';
import { checkout } from './Checkout/Checkout';
import { cart } from './cart/cart';
import { login } from './login/login';
import { dataservice } from './share/dataservice';
import { StarsComponent } from './productlist/stars/stars.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MyComponent,
    Store,
    ProductList,
    checkout,
    cart,
    login,
    StarsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'store', component: Store },
      { path: 'checkout', component: checkout },
      { path: 'login', component: login }
    ])
  ],
  providers: [dataservice],
  bootstrap: [AppComponent]
})
export class AppModule { }
