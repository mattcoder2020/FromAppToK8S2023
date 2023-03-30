import { IOrderItem } from "./OrderItem"

export class IOrder {
    customerName: string
    address: string
    email: string
    phone: string
    total: number
    fax: number
    paymentMethod: string
    orderStatus: number
    orderDate: string
    orderItems: IOrderItem[]
    id: number
  }
  
