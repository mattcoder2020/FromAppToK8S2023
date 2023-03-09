import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { v4 as uuidv4 } from 'uuid';
import { environment } from '../../environments/environment';
import { IBasket } from '../entity/IBasket';
import { IBasketItem } from '../entity/IBasketItem';
import { IProduct } from '../entity/IProduct';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private basket: IBasket;
  constructor(private http: HttpClient) { }

  
  getBasket() {
    let id = localStorage.getItem("basketid");
    if (id == null) {
      id = uuidv4();
      localStorage.setItem("basketid", id);
      this.basket.id = id;
    }
    else {
      this.http.get<(IBasket)>(environment.apiurlBasket + "/" + id).subscribe(response => {
        this.basket = response;
      })
      }
 
  }

  setBasket() {
    this.http.post(environment.apiurlBasket + "/", this.basket);
  }

  updateBasketItem(basketItem: IBasketItem)
  {
    var temp = this.basket.items.filter((e) => e.id == basketItem.id);
    if (temp.length > 0) { temp[0] = basketItem; }
    else (this.basket.items.push(basketItem));
  }


  ProductToBasketItem(product: IProduct): IBasketItem
  {
    return {
      id: product.id,
      name : product.name,
      price : product.price,
      productCategory : product.productCategory,
      productCategoryId: product.productCategoryId,
      quantity : 0
    };
  }




  
}
