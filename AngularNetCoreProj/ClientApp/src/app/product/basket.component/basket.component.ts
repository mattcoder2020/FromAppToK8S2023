import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from '../../entity/IBasket';
import { IBasketTotal } from "../../entity/IBasketTotal";
import { IBasketItem } from '../../entity/IBasketItem';
import { BasketService } from '../basket.service';
import { ToastrService } from 'ngx-toastr';
import { IOrder } from 'src/app/entity/IOrder';
import { OrderService } from 'src/app/order/orderservice';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit  {

  public basket: IBasket;
  public order: IOrder;
  public basket$: Observable<IBasket>;
  public basketTotalQuantity$: Observable<IBasketTotal>;
  constructor(private basketservice: BasketService, private orderservice: OrderService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
   
    this.basketservice.getBasket();
    this.order = new IOrder();
    //this.basket = this.basketservice.basket;
    this.basket$ = this.basketservice.basket$;
    this.basketTotalQuantity$ = this.basketservice.basketTotal$;

  }

  decrementItemQuantity(item: IBasketItem) {
    if (item.quantity > 0) item.quantity--;
    this.basketservice.updateBasketItem(item).
    subscribe
      (
        error => this.toastr.error("Failed to update the basket", "Error")
      );

  }

  incrementItemQuantity(item: IBasketItem) {
    item.quantity++;
    this.basketservice.updateBasketItem(item).subscribe
              (
          error => this.toastr.error("Failed to update the basket", "Error")
        );
      }

  placeorder(){
    this.orderservice.createorder(this.order).subscribe
    (
      success =>this.toastr.success("Order created successfully", "Success"),
      error => this.toastr.error(error.error.message, error.error.statuscode)
    );
 } 

}
