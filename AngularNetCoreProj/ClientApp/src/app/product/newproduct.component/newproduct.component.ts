import { Component,OnInit } from '@angular/core';
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
  selector: 'app-newproduct',
  templateUrl: './newproduct.component.html',
  styleUrls: ['./newproduct.component.scss']
})
export class NewproductComponent  implements OnInit {
  public productcategories: IProductCategory[];
  public product: IProduct;
  public updateresult: string;
  private basketitem: IBasketItem;
  private basketitemcount: number;

  constructor(private productservice: productservice, private basketservice: BasketService,
    private activatedRoute: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit() {
    this.product = new IProduct();
    this.productservice.getallCategory().subscribe(
      (temp: IProductCategory[]) => this.productcategories = temp,
      error => console.log(error));

  }

  Confirm() {
   // this.product = new IProduct();
   // this.product.id = 20;
   // this.product.name = "api test";
   // this.product.productCategoryId = 4;
   // this.product.price = 2000;

    
    this.productservice.createproduct(this.product).subscribe(
      success => this.toastr.success('Created Successful!', 'Product Create'),
      error => this.toastr.error(error.error.message, error.error.statuscode)
    )
  }

  Cancel() {

  }



}

