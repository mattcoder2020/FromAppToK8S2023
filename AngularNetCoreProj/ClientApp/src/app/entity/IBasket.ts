import { IBasketItem } from "./IBasketItem";

export class IBasket
{
  basketid: string;
  items: IBasketItem[] = [];
  subtotal: number;
  shipment: number;
  total: number;
}
