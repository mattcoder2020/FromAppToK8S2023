import { Product } from "./Product";

export class OrderItem {
    public id: number;
    public productId: number;
    public product?: Product;
    public quantity: number;
    public orderId: number;

    getTotal(): number {
        return this.quantity * this.product.price;
    }

}