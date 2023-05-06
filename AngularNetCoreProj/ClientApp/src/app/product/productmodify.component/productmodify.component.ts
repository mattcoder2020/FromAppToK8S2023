import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IBasketItem } from '../../entity/IBasketItem';
import { IProduct } from '../../entity/IProduct';
import { IProductCategory } from '../../entity/IProductCategory';
import { BasketService } from '../basket.service';
import { productservice } from '../productservice';

@Component({
  selector: 'app-productmodify',
  templateUrl: './productmodify.component.html',
  styleUrls: ['./productmodify.component.css']
})
export class ProductmodifyComponent implements OnInit {
  public productcategories: IProductCategory[];
  public product: IProduct;
  public updateresult: string;
  private basketitem: IBasketItem;
  private basketitemcount: number;

  constructor(private productservice: productservice, private basketservice: BasketService,
    private activatedRoute: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit() {
    this.productservice.getallCategory().subscribe(
      (temp: IProductCategory[])=> this.productcategories = temp,
      error => console.log(error));

    this.productservice.getbyid(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(
      (temp: IProduct) => this.product = temp,
      error => console.log(error));

    this.basketitem = this.basketservice.getBasketItem(+this.activatedRoute.snapshot.paramMap.get('id'));
    if (this.basketitem != null) {
      this.basketitemcount = this.basketitem.quantity;
    }
    else {
      this.basketitemcount = 0;
    }

  }

  Confirm() {
    this.updateresult = "";
    this.productservice.updateproduct(this.product).subscribe(
      //success => this.updateresult = "Updated",
      //error => this.updateresult = "Failed to update"

      success => this.toastr.success('Update Successful!', 'Product Update'),
      error => this.toastr.error(error.error.message, error.error.statuscode)
    )
  }

  incrementItemQuantity(product:IProduct) {
    this.basketitemcount++;
    this.basketitem = {
      id: product.id,
      name: product.name,
      price: product.price,
      quantity: this.basketitemcount,
      productCategory: product.productCategory.Description,
      productCategoryId: product.productCategoryId
      
    }

    this.basketservice.updateBasketItem(this.basketitem);
  }

  

  Cancel() {

  }

  

}
