import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketTotal } from '../../entity/IBasket';
import { IBasketItem } from '../../entity/IBasketItem';
import { BasketService } from '../basket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit  {

  public basket: IBasket;
  public basket$: Observable<IBasket>;
  public basketTotalQuantity$: Observable<IBasketTotal>;
  constructor(private basketservice: BasketService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
   
    this.basketservice.getBasket();
    //this.basket = this.basketservice.basket;
    this.basket$ = this.basketservice.basket$;
    this.basketTotalQuantity$ = this.basketservice.basketTotal$;

  }

  decrementItemQuantity(item: IBasketItem) {
    if (item.quantity > 0) item.quantity--;
    this.basketservice.updateBasketItem(item).subscribe
      (
        success=>
        error => this.toastr.error(error.error.message, error.error.statuscode)
      );

  }

  incrementItemQuantity(item: IBasketItem) {
    item.quantity++;
    this.basketservice.updateBasketItem(item).subscribe
      (
        success =>
          error => this.toastr.error(error.error.message, error.error.statuscode)
      );
  }
  

}
