import { IProduct } from "./IProduct";


export interface IBasketItem extends IProduct {
    quantity: number;
}
