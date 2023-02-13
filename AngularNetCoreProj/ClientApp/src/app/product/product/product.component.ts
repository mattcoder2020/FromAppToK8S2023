import { Component, Input, OnInit } from '@angular/core';
import { productservice } from '../../coremodule/productservice';
import { IProduct } from '../../entity/IProduct';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})


export class ProductComponent implements OnInit {
   @Input() product: IProduct;

  constructor() { }
  ngOnInit(): void {}

}
