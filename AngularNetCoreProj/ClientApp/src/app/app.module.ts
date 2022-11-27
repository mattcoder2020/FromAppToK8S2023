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
//import { Store } from './store/store';
//import { ProductList } from './store/productlist';
//import { checkout } from './store/Checkout';
//import { cart } from './store/cart';
//import { login } from './store/login';
//import { dataservice } from './share/dataservice';
//import { StarsComponent } from './store/stars/stars.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MyComponent
    //dataservice,
    //Store
    //ProductList,
    //checkout,
    //cart,
    //login,
    //StarsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'store', component: MyComponent }
      //{ path: 'checkout', component: checkout },
      //{ path: 'login', component: login }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
