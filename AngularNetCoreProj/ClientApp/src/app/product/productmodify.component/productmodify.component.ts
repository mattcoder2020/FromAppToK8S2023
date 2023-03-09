import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IBasket } from '../../entity/IBasket';
import { IBasketItem } from '../../entity/IBasketItem';
import { IProduct } from '../../entity/IProduct';
import { IProductCategory } from '../../entity/IProductCategory';
import { params } from '../../entity/params';
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

  constructor(private productservice: productservice, private basketservice: BasketService,
    private activatedRoute: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit() {
    this.productservice.getallCategory().subscribe(
      (temp: IProductCategory[])=> this.productcategories = temp,
      error => console.log(error));

    this.productservice.getbyid(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(
      (temp: IProduct) => this.product = temp,
      error => console.log(error));

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

  

  Cancel() {

  }

  

}
