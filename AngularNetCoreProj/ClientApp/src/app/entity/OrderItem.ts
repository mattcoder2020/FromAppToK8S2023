
export class IOrderItem {
  constructor(id, name, orderId, quantity, price, productCategory) {
    this.id = id;
    this.name = name;
    this.orderId = orderId;
    this.quantity = quantity;
    this.price = price;
    this.productCategory = productCategory;
  }
    orderId: number;
    quantity: number;
    name: string;
    price: number;
    productCategory: number;
    id?: number;
}
