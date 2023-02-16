import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { productservice } from '../productservice';
import { IProduct } from '../../entity/IProduct';
import { IProductCategory } from "../../entity/IProductCategory";

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  public products: IProduct[];
  public productcategories: IProductCategory[];

  constructor(private ps1: productservice) { }
  ngOnInit(): void {
    this.getallproduct();
    this.getallproductCategory();
  }

  getallproduct() {
    this.ps1.getall().subscribe((prods: IProduct[]) => { this.products = prods; },
      error => { console.log(error) });
  }

  getallproductCategory() {
    this.ps1.getallCategory().subscribe((prodcats: IProductCategory[]) => { this.productcategories = prodcats; },
      error => { console.log(error) });
  }
}





