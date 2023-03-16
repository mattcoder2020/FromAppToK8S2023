import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';
import { environment } from '../../environments/environment';
import { IBasket, IBasketTotal } from '../entity/IBasket';
import { IBasketItem } from '../entity/IBasketItem';
import { IProduct } from '../entity/IProduct';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: "root" })
export class BasketService implements OnInit
{

  public basket: IBasket;
  private basketid: string;
  private basketSource = new BehaviorSubject<IBasket>(null);
  private basketTotalSource = new BehaviorSubject<IBasketTotal>(null);
  basket$ = this.basketSource.asObservable();
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) {
   
    this.basketid = localStorage.getItem("basketid");
    if (this.basketid == null) {
      this.basketid = uuidv4();
      localStorage.setItem("basketid", this.basketid);
     
    }
    this.basket = new IBasket();
    this.basket.basketid = this.basketid;
  
  }

  ngOnInit(): void {
    
   }

  getBasket() {
    this.http.get<(IBasket)>(environment.apiurlBasket + "/" + this.basketid)
      .pipe(
        map((response: IBasket) => {
          if (response != null) {
            this.basketSource.next(response);
            this.basket = response;
            this.calculateTotalQuantity(response);
          }}
        )
    ).subscribe()
   }

  calculateTotalQuantity(basket: IBasket) {
    let totalquantity: number = basket.items.reduce((a, b) => a + b.quantity, 0);
    let baskettotal: IBasketTotal = new IBasketTotal();
    baskettotal.total = totalquantity ;
    this.basketTotalSource.next(baskettotal);

  }
  setBasket() {
    return this.http.post("http://localhost:5002/api/basket/", this.basket).pipe(map(
      sucess => {
        this.basketSource.next(this.basket);
        this.calculateTotalQuantity(this.basket);
      }
    ));
  }

  updateBasketItem(basketItem: IBasketItem)
  {
    var temp = this.basket.items.filter((e) => e.id == basketItem.id);
    if (temp.length > 0) {
      temp[0].quantity++;
    }
    else {
      basketItem.quantity = 1;
      this.basket.items.push(basketItem)
    };
    return this.setBasket();
  }


  ProductToBasketItem(product: IProduct): IBasketItem
  {
    return {
      id: product.id,
      name : product.name,
      price : product.price,
      productCategory : product.productCategory.description,
      productCategoryId: product.productCategoryId,
      quantity : 0
    };
  }
    
}
