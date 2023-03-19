import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IBasket } from '../../entity/IBasket';
import { IBasketItem } from '../../entity/IBasketItem';
import { IProduct } from '../../entity/IProduct';
import { BasketService } from '../basket.service';


@Component({
  selector: 'app-productdisplay',
  templateUrl: './productdisplay.component.html',
  styleUrls: ['./productdisplay.component.css']
})


export class ProductDisplayComponent implements OnInit {
   @Input() product: IProduct;

  constructor(private basketservice: BasketService, private router: Router
    , private toastr: ToastrService ) { }
  ngOnInit(): void { }

  AddToBasket() {
    let basketitem: IBasketItem;
    let temp = this.basketservice.basket.items.filter((e) => e.id == this.product.id);
    if (temp.length > 0) {
      basketitem = temp[0];
      basketitem.quantity++;
     
    }
    else {
      basketitem = this.basketservice.ProductToBasketItem(this.product);
      basketitem.quantity = 1;
    }
    this.basketservice.updateBasketItem(basketitem).subscribe(
      success =>
      error => this.toastr.error(error.error.message, error.error.statuscode)
    );
    //this.router.navigate(['basket']);
  }

}
