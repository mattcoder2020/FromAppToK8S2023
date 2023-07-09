import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ReactiveFormsModule } from '@angular/forms';


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
import { NewproductComponent } from './product/newproduct.component/newproduct.component';
import { InventoryComponent } from './inventory/inventory.component/inventory.component';

 

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CoreModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'product', loadChildren: () => import('./product/product.module').then(mod => mod.Productmodule) },
      { path: 'inventory', loadChildren: () => import('./inventory/inventorymodule/inventorymodule.module').then(mod => mod.InventorymoduleModule) },
      { path: 'core', loadChildren: () => import('./coremodule/core.module').then(mod => mod.CoreModule) },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'basket', component: BasketComponent },
      { path: 'newproduct', component: NewproductComponent }
   
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
