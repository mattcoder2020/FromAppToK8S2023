import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { error } from 'protractor';
import { IProduct } from '../../entity/IProduct';
import { IProductCategory } from '../../entity/IProductCategory';
import { params } from '../../entity/params';
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
  constructor(private productservice: productservice, private activatedRoute: ActivatedRoute) { }

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
      success => this.updateresult = "Updated",
      error => this.updateresult = "Failed to update"
    )

  }

  Cancel() {

  }

  

}
