import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';


//import { login } from './login/login';
// import { dataservice } from './storemodule/share/dataservice';
//import { StoreModule } from './storemodule/app.module';

import { CoreModule } from './coremodule/core.module';
//import { Productmodule } from './product/product.module';
//import { ProductRouteModule } from './product/product-route/product-route.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptor } from './coremodule/interceptors/error.interceptor';
import { LoadingInterceptor } from './coremodule/interceptors/loading.interceptor';
import { NgxSpinnerModule } from "ngx-spinner";
import { BasketComponent } from './product/basket.component/basket.component';

 

@NgModule({
  declarations: [
    AppComponent,
    //NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CoreModule,
    NgxSpinnerModule,
  //  ProductRouteModule,
  //  Productmodule,
   // StoreModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'product', loadChildren: () => import('./product/product.module').then(mod => mod.Productmodule) },
      { path: 'core', loadChildren: () => import('./coremodule/core.module').then(mod => mod.CoreModule) },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'basket', component: BasketComponent }
      
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
