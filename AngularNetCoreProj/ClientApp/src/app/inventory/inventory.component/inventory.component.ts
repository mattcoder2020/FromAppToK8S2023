import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IInventoryProduct } from '../../entity/IInventoryProduct';
import { IProductCategory } from '../../entity/IProductCategory';
import { params } from '../../entity/params';
import { productservice } from '../../product/productservice';
import { inventoryservice } from '../inventoryservice';

@Component({
  selector: 'app-inventory.component',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.scss']
})
export class InventoryComponent implements OnInit {
  public productcategories: IProductCategory[]
  public selectedproductcategoryid: number = 0;
  public products: any[];

  constructor(private ps1: inventoryservice, private toastr: ToastrService) {
  }

  ngOnInit(): void {
      this.getallproductCategory();
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

  getProductsByFiltration() {
    var p = new params();
    p.productcatetoryid = this.selectedproductcategoryid;
    this.ps1.getproductsbyfiltration(p).subscribe(
      (prods: IInventoryProduct[]) => { this.products = prods; },
      error => { console.log(error) }
    );
  }
  onQuantityChange(product: any, newQuantity: any) {
    product.quantity = newQuantity;
  }
  updatequantity(product: IInventoryProduct) {
    var myDiv = document.getElementById("quantity-" + product.id) as HTMLInputElement;
    //how to convert myDiv.innerText to number

    product.quantity = parseInt(myDiv.value);
    this.ps1.updateproduct(product).subscribe(
      success => this.toastr.success('Update Successful!', 'Update Quan'),
      error => this.toastr.error(error.error.message, error.error.statuscode)
    )
  }

  onNameChange(event: any) {
    console.log(`Name of product ${event} changed`);
  }

  onPriceChange(event: any) {
    console.log(`Price of product ${event} changed`);
  }

  onQuantityChange1(event: any) {
    console.log(`Quantity of product ${event} changed`);
  }

  addProduct() {
    this.products[0].name = "changed name";
    console.log(this.products[0].name + this.products[0].quantity);
  }
}
