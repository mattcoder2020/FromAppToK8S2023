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
import { IOrderItem } from '../../entity/OrderItem';
import { ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

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
  @ViewChild('myForm', { static: false }) myForm: NgForm;

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
    //how to remove the basket item from the basket if the quantity is 0

    if (item.quantity > 0) item.quantity--;
    if (item.quantity == 0) this.basketservice.removeItemFromBasket(item).subscribe
    (
      error => this.toastr.error("Failed to update the basket", "Error")
    )
    else
    {
    this.basketservice.updateBasketItem(item).
    subscribe
      (
        success =>
        error => this.toastr.error("Failed to update the basket", "Error")
      );
    }

  }

  incrementItemQuantity(item: IBasketItem) {
    item.quantity++;
    this.basketservice.updateBasketItem(item).subscribe
              (
          error => this.toastr.error("Failed to update the basket", "Error")
        );
      }

  placeorder() {
    const requiredFields = ['username', 'email','phone'];
    const invalidFields = requiredFields.filter(field => !this.order[field]);
    //declare a boolean variable to check whether the form is valid or not
    let isFormValid = true;

    if (this.myForm.valid)

      this.basketservice.basket.items.forEach((i) =>
        this.order.orderItems.push(new IOrderItem(i.id, i.name, this.order.id, i.quantity, i.price, i.productCategoryId)));
      this.orderservice.createorder(this.order).subscribe
        (success => {
          this.toastr.success("Order created successfully", "Success");
          this.basketservice.deleteBasket().subscribe(
            success =>
              error => this.toastr.error("Failed to delete the basket", "Error")
          )
        },
          error => this.toastr.error(error.error.message, error.error.statuscode)
        );
   
 } 

}
