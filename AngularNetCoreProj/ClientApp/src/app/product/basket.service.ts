import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';
import { environment } from '../../environments/environment';
import { IBasket } from '../entity/IBasket';
import { IBasketTotal } from "../entity/IBasketTotal";
import { IBasketItem } from '../entity/IBasketItem';
import { IProduct } from '../entity/IProduct';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: "root" })
export class BasketService implements OnInit
{
 
  public basket: IBasket;
  private basketid: string;
  private basketSource = new BehaviorSubject<IBasket>(null);
  public basketTotalSource = new BehaviorSubject<IBasketTotal>(null);
  basket$ = this.basketSource.asObservable();
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) {
   
    this.basketid = this.getBasketId();
    this.basket = new IBasket();
    this.basket.basketid = this.basketid;
    this.getBasket();
  
  }

  ngOnInit(): void {
    
  }

  getBasketId(): string {
    let id = localStorage.getItem("basketid");
    if (id == null) {
      id = uuidv4();
      localStorage.setItem("basketid", id);
    }
    return id;

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
    return this.http.post("http://localhost:5002/api/basket/", this.basket).pipe(
      map(
      sucess => {
        this.basketSource.next(this.basket);
        this.calculateTotalQuantity(this.basket);
      }
    ));
  }

  deleteBasket() {
    let url = "http://localhost:5002/api/basket/" + this.getBasketId();
    return this.http.delete(url).pipe(
      map(
      sucess => {
        this.basket = new IBasket();
        this.basketSource.next(this.basket);
        this.calculateTotalQuantity(this.basket);
      }
    ));
  }

  updateBasketItem(basketItem: IBasketItem)
  {
    var temp = this.basket.items.filter((e) => e.id == basketItem.id);
    if (temp.length > 0) {
      temp[0]=basketItem;
    }
    else {
      basketItem.quantity = 1;
      this.basket.items.push(basketItem)
    };
    return this.setBasket();
  }

  removeItemFromBasket(item: IBasketItem) {
    //write code to remove the item from the basket.items collection
    this.basket.items =     this.basket.items.filter((e) => e.id != item.id); 
    return this.setBasket();  
  }   
 

  getBasketItem(basketitemid: number) {
    var temp = this.basket.items.filter((e) => e.id == basketitemid);
    if (temp.length > 0) {
      return temp[0];
    }
    else
    {return null;}
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
