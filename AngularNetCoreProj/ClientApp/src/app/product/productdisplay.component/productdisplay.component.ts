import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    var basketitem = this.basketservice.ProductToBasketItem(this.product);
    basketitem.quantity = 1;
    this.basketservice.updateBasketItem(basketitem).subscribe(
      success =>
      error => this.toastr.error(error.error.message, error.error.statuscode)
    );;
    this.router.navigate(['basket']);
  }

}
