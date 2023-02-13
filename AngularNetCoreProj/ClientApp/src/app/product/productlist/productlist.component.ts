import { Component, OnInit } from '@angular/core';
import { productservice } from '../../coremodule/productservice';
import { IProduct } from '../../entity/IProduct';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  public products: IProduct[];

  constructor(private ps1: productservice) { }
  ngOnInit(): void {
    this.ps1.getall().subscribe((prods: IProduct[]) => { this.products = prods; },
      error => { console.log(error) });

    //this.products = this.ps1.products;
  }

}
