import { OrderItem } from "./OrderItem";

export class Order {
    id: number;
    date: Date;
    orderItems: Array<OrderItem> = new Array<OrderItem>();
    getTotal(): number {
        let total: number = 0;
        this.orderItems.forEach(e => total += e.quantity * e.product.price);
        return total;
    }
}
