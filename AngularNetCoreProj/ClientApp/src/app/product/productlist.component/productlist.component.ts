import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { productservice } from '../productservice';
import { IProduct } from '../../entity/IProduct';
import { IProductCategory } from "../../entity/IProductCategory";
import { params } from '../../entity/params';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {

  public products: IProduct[];
  public productcategories: IProductCategory[];
  public selectedproductcategoryid: number = 0;
  public sortOptions= [
    { name: "Price from low to high", value: "price_asc" },
    { name: "Price from high to low", value: "price_desc" },
    { name: "Name", value: "name" }]
  public selectedSort = "name";

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
    this.ps1.getallCategory().subscribe(
      (prodcats: IProductCategory[]) => this.productcategories = [{ Id: 0, Description: 'All' }, ...prodcats],
      error => { console.log(error) });
  }

  onCategorySelected(catid: number) {
    this.selectedproductcategoryid = catid;
    this.getProductsByFiltration();
  }

  onSortChange(value: string)
  {
    this.selectedSort = value;
    this.getProductsByFiltration();
  }

  getProductsByFiltration() {
    var p = new params();
    p.productcatetoryid = this.selectedproductcategoryid;
    p.orderby = this.selectedSort;
    this.ps1.getproductsbyfiltration(p).subscribe(
      (prods: IProduct[]) => { this.products = prods; },
      error => { console.log(error) }
    );
  }
}





