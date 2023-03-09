import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
   ) { }
  ngOnInit(): void { }
  AddToBasket() {
    var basketitem = this.basketservice.ProductToBasketItem(this.product);
    basketitem.quantity = 1;
    this.basketservice.updateBasketItem(basketitem);
    this.router.navigate(['basket']);
  }

}
