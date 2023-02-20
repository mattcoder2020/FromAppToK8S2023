import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { MyComponent } from './test-component/test.component';
//import { login } from './login/login';
 import { dataservice } from './storemodule/share/dataservice';
//import { StoreModule } from './storemodule/app.module';

import { CoreModule } from './coremodule/core.module';
import { Productmodule } from './product/product.module';
import { ProductRouteModule } from './product/product-route/product-route.module';
 

@NgModule({
  declarations: [
    AppComponent,
    //NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    //login,
    MyComponent
    
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CoreModule,
    ProductRouteModule,
    Productmodule,
   // StoreModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
     // { path: 'login', component: login },
      { path: 'fetch-data', component: FetchDataComponent }
      
    ])
  ],
  providers: [dataservice],
  bootstrap: [AppComponent]
})
export class AppModule { }
