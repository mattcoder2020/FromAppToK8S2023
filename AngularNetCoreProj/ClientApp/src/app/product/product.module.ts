import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { ProductDisplayComponent } from './productdisplay.component/productdisplay.component';
import { ProductlistComponent } from './productlist.component/productlist.component';
import { ProductmodifyComponent } from './productmodify.component/productmodify.component';

@NgModule({
  imports: [
    CommonModule, RouterModule, FormsModule
  ],
  declarations: [ProductDisplayComponent, ProductlistComponent, ProductmodifyComponent],
  exports: [ProductDisplayComponent, ProductlistComponent]
})
export class Productmodule { }
