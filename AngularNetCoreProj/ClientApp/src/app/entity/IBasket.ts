import { IBasketItem } from "./IBasketItem";

export interface IBasket
{
  id: string;
  items: IBasketItem[];
  subtotal: number;
  shipment: number;
  total: number;
}
