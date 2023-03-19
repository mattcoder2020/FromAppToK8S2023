import { IBasketItem } from "./IBasketItem";
import { IProductCategory } from "./IProductCategory";

export class IBasket
{
  basketid: string;
  items: IBasketItem[] = [];
  subtotal: number = 0 ;
  shipment: number= 0;
  total: number = 0;
}

